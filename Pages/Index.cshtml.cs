using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;

namespace NeilMacabulos.PrelimExam_.Pages
{
    public class StudentModel : PageModel
    {
        [BindProperty]
        public Student? ViewModel { get; set; }
        public object JsonConvert { get; private set; }

        public async IActionResult OnGet(string? firstName)
        {
            this.ViewModel = new Student();
            this.ViewModel.FirstName = firstName;

            var client = new RestSharp.Portable.HttpClient.RestClient("https://fcc-weather-api.glitch.me/api/");
            var request = new Restsharp.Portable.RestRequest("current?lat=14.8781&long=120.4546", RestSharp.Portable.Method.GET);
            //request.AddParameter(new Parameter() { Name = "lat", Value = "14.8781" });
            //request.AddParameter(new Parameter() { Name = "long", Value = "120.4546" });

            IRestResponse response = await client.Execute(request);

            var content = response.Content;

            var data = JsonConvert.DeserializeObject<WeatherData>(content);

            this.ViewModel.Weather = data;

            return Page();
        }

        public IActionResult OnPost()
        {
            var firstName = this.ViewModel?.FirstName;

            if (this.ViewModel?.DateOfBirth != null)
            {
                this.ViewModel.Age = DateTime.Now.Year - this.ViewModel?.DateOfBirth.Value.Year;

                string fileName = @"C:\Temp\" + firstName + ".json";

                try
                {
                    // Check if file already exists. If yes, delete it
                    if (System.IO.File.Exists(fileName))
                    {
                        System.IO.File.Delete(fileName);
                    }

                    // Create New file
                    using (FileStream fs = System.IO.File.Create(fileName))
                    {
                        var fileText = "{firstName:" + this.ViewModel.FirstName + ",";
                        fileText = fileText + "age:" + this.ViewModel.Age + ",";
                        fileText = fileText + "dateOfBirth:" + this.ViewModel.DateOfBirth + ",";

                        Byte[] text = new UTF8Encoding(true).GetBytes(fileText);
                        fs.Write(text, 0, text.Length);
                    }

                }

            }

            return Page();
        }

        public class Student
        {
            public string? FirstName { get; set; }
            public DateTime? DateOfBirth { get; set; }
            public int? Age { get; set; }
            public WeatherData Weather { get; set; }
        }

        public class WeatherData
        {
            public WeatherMain Main { get; set; }
            public List<WeatherDetails> Weather { get; set; }
        }

        public class WeatherMain
        {
            public string? Temp { get; set; }

            [JsonPropertyName("feels_like")]
            public string? FeelsLike { get; set; }
            public string? Pressure { get; set; }
            public string? Humidity { get; set; }
        }

        public class WeatherDetails
        {
            public string? NameofLocation { get; set; }
            public string? Main { get; set; }
            public string? Description { get; set; }
            public string? Icon { get; set; }
        }
    }
}