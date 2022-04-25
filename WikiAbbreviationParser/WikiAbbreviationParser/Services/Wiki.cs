using Newtonsoft.Json;
using RestSharp;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WikiAbbreviationParser.Models;

namespace WikiAbbreviationParser.Services
{
    public class Wiki
    {
        private RestClient _client;
        private Regex _categoryTreeRegex = new Regex("{\"categorytree\":{\"\\*\":\"(.*)\"}}");
        private Regex _categoryMembersRegex = new Regex(
            "{\"batchcomplete\":\"\",\"limits\":{\"categorymembers\":500},\"query\":{\"categorymembers\":(.+)}}"
        );
        private Regex _pageExtractRegex = new Regex("{\"batchcomplete\":\"\",\"query\":{\"pages\":{\"page_id\":(.+)}}}");

        public event EventHandler<Category> CategoryRetieveStart;
        public event EventHandler<Category> CategoryRetieveEnd;

        public string Address { get; private set; }

        public Wiki(string address)
        {
            Address = address;
            _client = new RestClient(address);
        }

        public async Task RetrieveCategoryContents(Category rootCategory)
        {
            CategoryRetieveStart?.Invoke(this, rootCategory);

            await RetrieveCategoryPages(rootCategory);

            var request = new RestRequest();

            request.AddParameter("format", "json");
            request.AddParameter("action", "categorytree");
            request.AddParameter("category", rootCategory.Title);

            var response = await _client.GetAsync(request);
            var categoriesHtml = _categoryTreeRegex.Match(response.Content).Groups[1].Value.Replace("\\", "");
            rootCategory.SubCategories = Category.CreateCategoriesFromHtml(categoriesHtml);

            foreach(var category in rootCategory.SubCategories)
            {
                await RetrieveCategoryContents(category);
            }

            CategoryRetieveEnd?.Invoke(this, rootCategory);
        }

        public async Task RetrieveCategoryPages(Category category)
        {
            var request = new RestRequest();

            request.AddParameter("format", "json");
            request.AddParameter("action", "query");
            request.AddParameter("list", "categorymembers");
            request.AddParameter("cmlimit", "max");
            request.AddParameter("cmtype", "page");
            request.AddParameter("cmtitle", $"Category:{category.Title}");

            var response = await _client.GetAsync(request);
            var categoryMembersJsonArray = _categoryMembersRegex.Match(response.Content).Groups[1].Value;

            category.Pages = JsonConvert.DeserializeObject<Page[]>(categoryMembersJsonArray);

            foreach(var page in category.Pages)
            {
                await RetrievePageExtract(page);
            }
        }

        public async Task RetrievePageExtract(Page page)
        {
            var request = new RestRequest();

            request.AddParameter("format", "json");
            request.AddParameter("action", "query");
            request.AddParameter("prop", "extracts");
            request.AddParameter("explaintext", true);
            request.AddParameter("pageids", page.Id);

            var response = await _client.GetAsync(request);
            var content = response.Content.Replace($"\"{page.Id}\"", "\"page_id\"");
            var extractJson = _pageExtractRegex.Match(content).Groups[1].Value;

            page.Extract = JsonConvert.DeserializeObject<Extract>(extractJson);
        }
    }
}
