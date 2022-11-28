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
            gexfDocument.Graph.Nodes.AddRange(nodes);
        }

        public void SetNodesColors(Color primeColor, Color nonPrimeColor)
        {
            foreach (var item in gexfDocument.Graph.Nodes)
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

        public void AddEdges(params GexfEdge[] edges)
        {
            gexfDocument.Graph.Edges.AddRange(edges);
        }

        public void Save(string path)
        {
            gexfDocument.Save(path);
        }

        private void SetParameters()
        {
            gexfDocument.Meta.LastModified = DateTimeOffset.Now;
            gexfDocument.Meta.Creator = Environment.UserName;
            gexfDocument.Graph.IdType = GexfIdType.Integer;
            gexfDocument.Graph.Mode = GexfModeType.Static;
            gexfDocument.Graph.DefaultedEdgeType = GexfEdgeType.Directed;
        }
    }
}
