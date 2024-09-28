using RestSharp;

namespace SWAPI_Test
{
    internal class RequestHelper
    {
        public RestResponse GetResponse(string url)
        {
            RestClient client = new RestClient(url);

            RestRequest restRequest = new RestRequest(url, Method.Get);
            return client.Execute(restRequest);
        }
    }
}
