using Newtonsoft.Json;

namespace WikiAbbreviationParser.Models
{
    public class Extract
    {
        [JsonProperty("pageid")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("extract")]
        public string Content { get; set; }
    }
}
