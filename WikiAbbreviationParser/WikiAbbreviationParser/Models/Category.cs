using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WikiAbbreviationParser.Models
{
    public class Category
    {
        private static Regex _categoryInfoRegex = new Regex("<a href=\"(.+?)\" title=\"Category:(.+?)\">(.+?)</a>");

        public string Link { get; set; }
        public string Title { get; set; }
        public string DisplayName { get; set; }
        public IList<Category> SubCategories { get; set; }
        public IList<Page> Pages { get; set; }

        private Category() { }

        public static Category CreateRootCategory(string title)
        {
            return new Category() { Title = title, DisplayName = title };
        }
        
        public static IList<Category> CreateCategoriesFromHtml(string html)
        {
            var matches = _categoryInfoRegex.Matches(html);
            var categories = new List<Category>();

            for(int i = 0; i < matches.Count; ++i)
            {
                var match = matches[i];
                var category = new Category()
                {
                    Link = match.Groups[1].Value,
                    Title = match.Groups[2].Value,
                    DisplayName = match.Groups[3].Value
                };
                categories.Add(category);
            }

            return categories;
        }

        public IList<Category> GetAllCategories()
        {
            return SubCategories.SelectMany(category => category.GetAllCategories()).Append(this).ToArray();
        }

        public IList<Page> GetAllPages()
        {
            return GetAllCategories().SelectMany(category => category.Pages).ToArray();
        }

        public override string ToString()
        {
            return $"{DisplayName}; {SubCategories.Count} subcategories; {Pages.Count} pages;";
        }
    }
}
