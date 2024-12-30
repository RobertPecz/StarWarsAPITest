using NUnit.Framework;
using RestSharp;
using SWAPI_Test.Helpers;
using SWAPI_Test.Urls;

namespace SWAPI_Test.APITests
{
    public class SmokeTests
    {
        [TestCase]
        [TestCase(URL.Films)]
        [TestCase(URL.People)]
        [TestCase(URL.Planets)]
        [TestCase(URL.Species)]
        [TestCase(URL.Starships)]
        [TestCase(URL.Vehicles)]
        public void GetStarWarsResources(string resource = null)
        {
            var url = URL.RootUrl + resource;
            RequestHelper request = new RequestHelper();

            RestResponse restResponse = request.GetResponse(url);
        }      
    }
}