using System;
using System.Collections.Generic;

namespace WikiAbbreviationParser.Models
{
    public interface IEdgeWeight
    {
        int Weight { get; }
    }

    public class Graph<TVertex, TEdgeWeight> where TEdgeWeight : IEdgeWeight
    {
        private IDictionary<TVertex, int> _vertexIndex = new Dictionary<TVertex, int>();
        private TEdgeWeight[,] _matrixOfAjecency;

        public Graph(IList<TVertex> vertexes, Func<TVertex, TVertex, TEdgeWeight> edgeWeightGenerator)
        {
            _matrixOfAjecency = new TEdgeWeight[vertexes.Count, vertexes.Count];

            for (int i = 0; i < vertexes.Count; ++i)
            {
                for (int j = i + 1; j < vertexes.Count; ++j)
                {
                    this[vertexes[i], vertexes[j]] = edgeWeightGenerator(vertexes[i], vertexes[j]);
                }
            }
        }

        public TEdgeWeight this[TVertex vertex, TVertex vertexOther]
        {
            get => _matrixOfAjecency[GetVertexIndex(vertex), GetVertexIndex(vertexOther)];
            set
            {
                var index = GetVertexIndex(vertex);
                var indexOther = GetVertexIndex(vertexOther);

                _matrixOfAjecency[index, indexOther] = value;
                _matrixOfAjecency[indexOther, index] = value;
            }
        }

        private int GetVertexIndex(TVertex vertex)
        {
            if(!_vertexIndex.ContainsKey(vertex))
            {
                _vertexIndex.Add(vertex, _vertexIndex.Count);
            }

            return _vertexIndex[vertex];
        }
    }
}
