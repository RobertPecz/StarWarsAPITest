using SWAPI_Test.Data;

namespace SWAPI_Test.Helpers
{
    internal class SimpleWookieResponseDataPairer
    {
        public Dictionary<string, string> DataPairs = new Dictionary<string, string>();

        public SimpleWookieResponseDataPairer()
        {
            JSONPersonData jSONPersonData = new JSONPersonData();
            JSONPersonDataWookie jSONPersonDataWookie = new JSONPersonDataWookie();

            var jsonPersonDataProperties = jSONPersonData.GetType().GetProperties();
            var jsonPersonDataWookiesProperties = jSONPersonDataWookie.GetType().GetProperties();

            for (int i = 0; i < jsonPersonDataProperties.Length; i++)
            {
                DataPairs.Add(jsonPersonDataProperties[i].Name, jsonPersonDataWookiesProperties[i].Name);
            }
        }
    }
}
