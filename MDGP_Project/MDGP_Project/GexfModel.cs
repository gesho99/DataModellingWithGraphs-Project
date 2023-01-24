namespace MDGP_Project
{
    using System.Drawing;
    using Gexf;
    using Gexf.Visualization;

    public class GexfModel
    {
        private readonly GexfDocument gexfDocument;

        public GexfModel(GexfDocument gexfDocument)
        {
            this.gexfDocument = gexfDocument;
            SetParameters();
        }

        public void AddNodes(IEnumerable<GexfNode> nodes)
        {
            this.gexfDocument.Graph.Nodes.AddRange(nodes);
        }

        /*public void SetNodesColors(Color primeColor, Color nonPrimeColor)
        {
            foreach (var item in this.gexfDocument.Graph.Nodes)
            {
                if (Utils.IsPrime(int.Parse(item.Id.ToString())))
                {
                    item.Color(primeColor);
                }
                else
                {
                    item.Color(nonPrimeColor);
                }
            }
        }
*/
        public void AddEdges(params GexfEdge[] edges)
        {
            this.gexfDocument.Graph.Edges.AddRange(edges);
        }

        public void Save(string path)
        {
            this.gexfDocument.Save(path);
        }

        public GexfModel ProcessVisualization(GexfModel gexfModel, ResultedGraph resultedGraph)
        {
            var nodes = resultedGraph.Graph.Keys
                .Select(key =>
                    new GexfNode(key)
                    {
                        Label = key
                    })
                .ToList();

            gexfModel.AddNodes(nodes);
            // gexfModel.SetNodesColors(Color.Green, Color.Red);

            var counterForEdgeID = 0;
            var counter = 1;
            foreach (string node in resultedGraph.Graph.Keys)
            {
                var nodeValues = resultedGraph.Graph.GetValueOrDefault(node);
                var counterForEdgesValues = 0;

                foreach (string nodeValue in nodeValues)
                {
                    GexfEdge edge = new GexfEdge(counterForEdgeID, node, nodeValue);
                    edge.Weight = resultedGraph.Edges.GetValueOrDefault(counter)[counterForEdgesValues];
                    gexfModel.AddEdges(edge);

                    counterForEdgeID++;
                    counterForEdgesValues++;
                }

                counter++;
            }

            return gexfModel;
        }

        public GexfModel ProcessVisualization(GexfModel gexfModel, List<Tuple<string, string, GexfFloat>> graph, Dictionary<GexfEdge, float> edgesWithWeights)
        {
            var leftNodes = graph
                .Select(key =>
                new GexfNode(key.Item1)
                {
                    Label = key.Item1
                })
                .ToList();

            var rightNodes = graph
                .Select(key =>
                new GexfNode(key.Item2)
                {
                    Label = key.Item2
                })
                .ToList();

            leftNodes.AddRange(rightNodes);

            List<GexfNode> allNodes = leftNodes;

            allNodes = allNodes.Distinct().ToList();

            gexfModel.AddNodes(allNodes);

            var counterForEdgeID = 0;
            foreach (var tuple in graph)
            {
                GexfEdge edge = new GexfEdge(counterForEdgeID, tuple.Item1, tuple.Item2);
                edge.Weight = tuple.Item3;
                gexfModel.AddEdges(edge);

                counterForEdgeID++;
            }

            return gexfModel;
        }

        private void SetParameters()
        {
            this.gexfDocument.Meta.LastModified = DateTimeOffset.Now;
            this.gexfDocument.Meta.Creator = Environment.UserName;
            this.gexfDocument.Graph.IdType = GexfIdType.Integer;
            this.gexfDocument.Graph.Mode = GexfModeType.Static;
            this.gexfDocument.Graph.DefaultedEdgeType = GexfEdgeType.Undirected;
        }
    }
}
