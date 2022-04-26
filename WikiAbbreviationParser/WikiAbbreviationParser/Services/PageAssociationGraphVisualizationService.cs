using QuickGraph.Graphviz;
using QuickGraph.Graphviz.Dot;
using WikiAbbreviationParser.Models;

namespace WikiAbbreviationParser.Services
{
    public class PageAssociationGraphVisualizationService : GraphVisualizationService<Page, PageAssociationGraphEdgeWeight>
    {
        protected override void FormatEdge(FormatEdgeEventArgs<Page, PageAssociationGraphEdgeWeight> e)
        {
            e.EdgeFormatter.Weight = e.Edge.Weight;

            var label = e.Edge.GetAbbreviationIntersectionString();
            e.EdgeFormatter.Label = new GraphvizEdgeLabel() { Value = label };
        }

        protected override void FormatVertex(FormatVertexEventArgs<Page> e)
        {
            e.VertexFormatter.Label = e.Vertex.Title;
        }
    }
}
