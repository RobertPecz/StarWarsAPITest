
namespace SWAPI_Test
{
    internal class UrlHandler
    {
        public string GetUrlBeforeCharacter(string url, char endCharacter)
        {
            int index = url.IndexOf(endCharacter);
            if (index > 0)
            {
                return url.Substring(0, index);
            }
            else
            {
                return url;
            }
        }
    }
}
