namespace RestSharp
{
    internal class Portable
    {
        internal class HttpClient
        {
            internal class RestClient
            {
                private string v;

                public RestClient(string v)
                {
                    this.v = v;
                }

                internal object Execute(Restsharp.Portable.RestRequest request)
                {
                    throw new NotImplementedException();
                }
            }
        }
    }
}