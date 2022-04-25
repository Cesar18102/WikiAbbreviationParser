using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace WikiAbbreviationParser.Models
{
    public class Page
    {
        [JsonProperty("pageid")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        public Extract Extract { get; set; }
        public IDictionary<string, int> AbbreviationCounter { get; set; }

        public int? UniqueAbbreviationsCount => AbbreviationCounter?.Keys.Count;
        public int? TotalAbbreviationsCount => AbbreviationCounter?.Sum(count => count.Value);

        public override string ToString()
        {
            var counter = AbbreviationCounter == null ? "" : 
                $"; {UniqueAbbreviationsCount} unique abbreviations; {TotalAbbreviationsCount} total abbreviations;";

            return $"#{Id} {Title}{counter}";
        }
    }
}
