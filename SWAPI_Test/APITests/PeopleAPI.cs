using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using SWAPI_Test.Data;
using SWAPI_Test.Helpers;

namespace SWAPI_Test.APITests
{
    public class PeopleAPI
    {
        [TestCase(1, false)]
        [TestCase(2, true)]
        [TestCase(1000, false)]
        public void GetSWPeople(int id, bool wookieFormat)
        {
            var url = "https://swapi.dev/api/people/" + id.ToString();
            RequestHelper request = new RequestHelper();

            RestResponse restResponse = request.GetResponse(url);

            Assert.That(restResponse.StatusCode, Is.Not.Null);
            JSONPersonData jSONPersonData = JsonConvert.DeserializeObject<JSONPersonData>(restResponse.Content);
            JObject jsonObject = JObject.Parse(restResponse.Content);
            if (restResponse.StatusCode == HttpStatusCode.OK && restResponse.Content != null && wookieFormat)
            {
                SimpleWookieResponseDataPairer simpleWookieResponseDataPairer = new SimpleWookieResponseDataPairer();
                url = url + "/?format=wookiee";
                restResponse = request.GetResponse(url);

                IList<JToken> species = jsonObject["species"].Children().ToList();
                foreach (JToken speciesItem in species)
                {
                    if ((string)speciesItem == "https://swapi.dev/api/species/2/")
                    {
                        JSONPersonDataWookie jSONPersonDataWookie = JsonConvert.DeserializeObject<JSONPersonDataWookie>(restResponse.Content);
                        jSONPersonDataWookie.whrascwo = (string)jsonObject[simpleWookieResponseDataPairer.DataPairs.FirstOrDefault(element => element.Value == "whrascwo").Key];
                    }
                }
            }
            else if (restResponse.StatusCode == HttpStatusCode.OK && restResponse.Content != null && !wookieFormat)
            {
                Assert.That(jSONPersonData.edited >= jSONPersonData.created);
            }
            else
            {
                Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
        }

        [TestCase]
        public void GetSWPeople()
        {
            var url = "https://swapi.dev/api/people/?page=1&pageSize=10";
            RequestHelper request = new RequestHelper();

            RestResponse restResponse = request.GetResponse(url);
            Assert.That(restResponse.StatusCode, Is.Not.Null);
            Assert.That(restResponse.StatusCode == HttpStatusCode.OK);

            JSONPersonBulkData jSONPersonBulkData = JsonConvert.DeserializeObject<JSONPersonBulkData>(restResponse.Content);

            Assert.That(jSONPersonBulkData.results.Count, Is.EqualTo(10));

            if (jSONPersonBulkData.next != null)
            {
                restResponse = request.GetResponse(jSONPersonBulkData.next);
                Assert.That(restResponse.StatusCode, Is.Not.Null);
            }
            else if (jSONPersonBulkData.previous != null)
            {
                restResponse = request.GetResponse(jSONPersonBulkData.previous);
                Assert.That(restResponse.StatusCode, Is.Not.Null);
            }
        }

        [TestCase("luke")]
        [TestCase("c")]
        [TestCase("aaaa")] //failing as the invalid request respond 200.      
        public void GetSWPeopleSearch(string search)
        {
            var url = "https://swapi.dev/api/people/?search=" + search.ToLowerInvariant();
            RequestHelper request = new RequestHelper();

            RestResponse restResponse = request.GetResponse(url);
            Assert.That(restResponse.StatusCode, Is.Not.Null);

            JSONPersonBulkData jSONPersonBulkData = JsonConvert.DeserializeObject<JSONPersonBulkData>(restResponse.Content);

            if (jSONPersonBulkData.count == 0)
            {
                Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            }
            else
            {
                Assert.That(restResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
        }
    }
}