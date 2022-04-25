using QuickGraph;
using QuickGraph.Graphviz;
using QuickGraph.Graphviz.Dot;
using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using WikiAbbreviationParser.Models;

namespace WikiAbbreviationParser.Services
{
    public abstract class GraphVisualizationService<TVertex, TEdge> where TEdge : IEdgeWeight, IEdge<TVertex>
    {
        private static string TEMP_FILES_DIR = Environment.CurrentDirectory + "\\temp";

        public Bitmap GetGraphVisualization(Graph<TVertex, TEdge> graph, string name)
        {
            if(!Directory.Exists(TEMP_FILES_DIR))
            {
                Directory.CreateDirectory(TEMP_FILES_DIR);
            }

            var graphResourcesDir = $"{TEMP_FILES_DIR}\\{graph.Id}";
            if (Directory.Exists(graphResourcesDir))
            {
                Directory.Delete(graphResourcesDir);
            }
            Directory.CreateDirectory(graphResourcesDir);

            var tempGraph = new AdjacencyGraph<TVertex, TEdge>();
            tempGraph.AddVerticesAndEdgeRange(graph.Edges);

            var exportGraphAlgorithm = new GraphvizAlgorithm<TVertex, TEdge>(tempGraph);

            exportGraphAlgorithm.FormatVertex += (sender, e) => FormatVertex(e);
            exportGraphAlgorithm.FormatEdge += (sender, e) => FormatEdge(e);

            var result = exportGraphAlgorithm.Generate(new GraphRenderer(), graphResourcesDir + $"\\{name}");

            return Bitmap.FromFile(result) as Bitmap;
        }

        protected abstract void FormatEdge(FormatEdgeEventArgs<TVertex, TEdge> e);
        protected abstract void FormatVertex(FormatVertexEventArgs<TVertex> e);

        private sealed class GraphRenderer : IDotEngine
        {
            public string Run(GraphvizImageType imageType, string dot, string outputFileName)
            {
                File.WriteAllText(outputFileName, dot);

                var args = $"\"{outputFileName}\" -Tjpg -O";
                Process.Start(ConfigurationManager.AppSettings["graphVizLocation"] + "/dot.exe", args).WaitForExit();

                return $"{outputFileName}.jpg";
            }
        }
    }
}
