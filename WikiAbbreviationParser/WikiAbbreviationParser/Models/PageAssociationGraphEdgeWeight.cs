using System;
using System.Collections.Generic;
using System.Linq;

namespace WikiAbbreviationParser.Models
{
    public class PageAssociationGraphEdgeWeight : IEdgeWeight
    {
        public IReadOnlyDictionary<string, int> AbbreviationsIntersection { get; private set; }
        public int Weight { get; private set; }

        public PageAssociationGraphEdgeWeight(Page page, Page pageOther)
        {
            AbbreviationsIntersection = GetAbbreviationsIntersection(page, pageOther);
            Weight = AbbreviationsIntersection.Sum(intersection => intersection.Value);
        }

        private Dictionary<string, int> GetAbbreviationsIntersection(Page page, Page pageOther)
        {
            var abbreviationsIntersection = new Dictionary<string, int>();
            
            foreach(var abbreviation in page.AbbreviationCounter)
            {
                if(pageOther.AbbreviationCounter.ContainsKey(abbreviation.Key))
                {
                    var minCounterValue = Math.Min(abbreviation.Value, pageOther.AbbreviationCounter[abbreviation.Key]);
                    abbreviationsIntersection.Add(abbreviation.Key, minCounterValue);
                }
            }

            return abbreviationsIntersection;
        }
    }
}
