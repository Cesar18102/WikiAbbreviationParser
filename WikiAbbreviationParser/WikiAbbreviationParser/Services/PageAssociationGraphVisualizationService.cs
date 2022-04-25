using QuickGraph.Graphviz;
using WikiAbbreviationParser.Models;

namespace WikiAbbreviationParser.Services
{
    public class PageAssociationGraphVisualizationService : GraphVisualizationService<Page, PageAssociationGraphEdgeWeight>
    {
        protected override void FormatEdge(FormatEdgeEventArgs<Page, PageAssociationGraphEdgeWeight> e)
        {
            
        }

        protected override void FormatVertex(FormatVertexEventArgs<Page> e)
        {
            e.VertexFormatter.Label = e.Vertex.Title;
        }
    }
}
