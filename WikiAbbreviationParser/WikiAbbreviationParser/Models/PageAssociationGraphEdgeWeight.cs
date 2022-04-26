using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WikiAbbreviationParser.Models
{
    public class PageAssociationGraphEdgeWeight : IEdgeWeight, IEdge<Page>
    {
        public IReadOnlyDictionary<string, int> AbbreviationsIntersection { get; private set; }
        public int Weight { get; private set; }

        public Page Source { get; private set; }
        public Page Target { get; private set; }

        public PageAssociationGraphEdgeWeight(Page page, Page pageOther)
        {
            AbbreviationsIntersection = GetAbbreviationsIntersection(page, pageOther);
            Weight = AbbreviationsIntersection.Sum(intersection => intersection.Value);

            Source = page;
            Target = pageOther;
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

        public string GetAbbreviationIntersectionString()
        {
            var intersections = AbbreviationsIntersection.Select(
                intersection => $"{intersection.Key}: {intersection.Value}"
            ).ToArray();

            return string.Join("; ", intersections);
        }

        public override string ToString()
        {
            var intersectionsString = GetAbbreviationIntersectionString();
            return $"{Source.Title} - {Target.Title}; Weight = {Weight}; {intersectionsString}";
        }
    }
}
