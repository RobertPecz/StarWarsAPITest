namespace SWAPI_Test
{
    internal class JSONPersonBulkData
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<JSONPersonData> results { get; set; }

    }
}
