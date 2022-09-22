namespace Restsharp
{
    internal class Portable
    {
        internal class RestRequest
        {
            private string v;
            private object gET;

            public RestRequest(string v, object gET)
            {
                this.v = v;
                this.gET = gET;
            }
        }
    }
}