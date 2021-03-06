using GalaSoft.MvvmLight.Ioc;
using QuickGraph;
using QuickGraph.Graphviz;
using QuickGraph.Graphviz.Dot;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using WikiAbbreviationParser.Extensions;
using WikiAbbreviationParser.Models;
using WikiAbbreviationParser.Services;

namespace WikiAbbreviationParser
{
    public partial class Form1 : Form
    {
        private Wiki Wiki => SimpleIoc.Default.GetInstance<Wiki>();
        private PageAssociationGraphVisualizationService PageAssociationGraphVisualizationService => 
            SimpleIoc.Default.GetInstance<PageAssociationGraphVisualizationService>();

        public Form1()
        {
            InitializeComponent();

            SimpleIoc.Default.Register(() => new Wiki("https://en.wikipedia.org/w/api.php"));
            SimpleIoc.Default.Register<AbbreviationSearchService>();
            SimpleIoc.Default.Register<PageAssociationGraphVisualizationService>();

            Wiki.CategoryRetieveStart += Wiki_CategoryRetieveStart;
            Wiki.CategoryRetieveEnd += Wiki_CategoryRetieveEnd;

            ModelsExtensions.StartParsePageAbbreviations += ModelsExtensions_StartParsePageAbbreviations;
            ModelsExtensions.EndParsePageAbbreviations += ModelsExtensions_EndParsePageAbbreviations;
        }

        private void ModelsExtensions_EndParsePageAbbreviations(object sender, Page e) => Log($"Parsed page: {e}");
        private void ModelsExtensions_StartParsePageAbbreviations(object sender, Page e) => Log($"Started to parse page {e}");

        private void Wiki_CategoryRetieveStart(object sender, Category e) => Log($"Started to retrieve category {e.DisplayName}");
        private void Wiki_CategoryRetieveEnd(object sender, Category e) => Log($"Finished to retrieve category {e}");

        private void Log(string log)
        {
            var dateTime = DateTime.Now;
            LogTextBox.AppendText($"{dateTime.ToShortDateString()} {dateTime.ToShortTimeString()}: {log}\n");
        }

        private async void StartButton_Click(object sender, EventArgs e)
        {
            var rootCategory = Category.CreateRootCategory(RootCategoryTextBox.Text);

            await Wiki.RetrieveCategoryContents(rootCategory);
            await rootCategory.RetrieveAbbreviations();

            var pageAssociationGraph = new Graph<Page, PageAssociationGraphEdgeWeight>(
                rootCategory.GetAllPages(), 
                (page, pageOther) => new PageAssociationGraphEdgeWeight(page, pageOther)
            );

            var visualization = PageAssociationGraphVisualizationService.GetGraphVisualization(
                pageAssociationGraph, "Page Association"
            );

            PageAssociationGraphPictureBox.Image = visualization;
        }
    }
}
