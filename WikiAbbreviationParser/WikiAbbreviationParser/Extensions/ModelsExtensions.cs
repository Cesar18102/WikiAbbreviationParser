using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WikiAbbreviationParser.Models;
using WikiAbbreviationParser.Services;

namespace WikiAbbreviationParser.Extensions
{
    public static class ModelsExtensions 
    {
        public static event EventHandler<Page> StartParsePageAbbreviations;
        public static event EventHandler<Page> EndParsePageAbbreviations;

        private static AbbreviationSearchService AbbreviationSearchService => SimpleIoc.Default.GetInstance<AbbreviationSearchService>();

        public static async Task RetrieveAbbreviations(this Category category)
        {
            foreach (var page in category.Pages)
            {
                await page.RetrieveAbbreviations();
            }

            foreach(var subCategory in category.SubCategories)
            {
                await subCategory.RetrieveAbbreviations();
            }
        }

        public static async Task RetrieveAbbreviations(this Page page)
        {
            StartParsePageAbbreviations?.Invoke(null, page);
            page.AbbreviationCounter = await AbbreviationSearchService.GetAbbreviations(page.Extract.Content);
            EndParsePageAbbreviations?.Invoke(null, page);
        }
    }
}
