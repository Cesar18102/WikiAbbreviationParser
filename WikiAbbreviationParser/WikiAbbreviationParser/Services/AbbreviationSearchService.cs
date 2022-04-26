using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WikiAbbreviationParser.Services
{
    public class AbbreviationSearchService
    {
        private RestClient _client = new RestClient("https://www.abbreviations.com/serp.php/");

        private Regex _abbreviationRegex = new Regex("([A-Z]+([a-z\\&\\-\\.]*[A-Z0-9]+)+)");
        private Regex _abbreviationCheckRegex = new Regex("We've got <strong>(\\d+)</strong> definition");

        public async Task<IDictionary<string, int>> GetAbbreviations(string text)
        {
            var invalidAbbreviations = new HashSet<string>();
            var abbreviationsCounter = new Dictionary<string, int>();

            var abbreviations = _abbreviationRegex.Matches(text);

            for(int i = 0; i < abbreviations.Count; ++i)
            {
                var abbreviation = abbreviations[i].Groups[1].Value;

                if(abbreviationsCounter.ContainsKey(abbreviation))
                {
                    abbreviationsCounter[abbreviation]++;
                    continue;
                }

                if(invalidAbbreviations.Contains(abbreviation))
                {
                    continue;
                }

                if(await IsAbbreviationValid(abbreviation))
                {
                    abbreviationsCounter.Add(abbreviation, 1);
                }
                else
                {
                    invalidAbbreviations.Add(abbreviation);
                }
            }

            return abbreviationsCounter;
        }

        private async Task<bool> IsAbbreviationValid(string abbreviation)
        {
            var request = new RestRequest();
            request.AddParameter("st", abbreviation.Replace("&", "%26"));

            var response = await _client.GetAsync(request);
            var match = _abbreviationCheckRegex.Match(response.Content);

            if (match.Groups.Count <= 1)
            {
                return false;
            }

            var definitionsCount = match.Groups[1].Value;
            return Convert.ToInt32(definitionsCount) != 0;
        }
    }
}
