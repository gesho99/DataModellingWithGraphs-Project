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

        public void SetNodesColors(Color primeColor, Color nonPrimeColor)
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

        public void AddEdges(params GexfEdge[] edges)
        {
            this.gexfDocument.Graph.Edges.AddRange(edges);
        }

        public void Save(string path)
        {
            this.gexfDocument.Save(path);
        }

        private void SetParameters()
        {
            this.gexfDocument.Meta.LastModified = DateTimeOffset.Now;
            this.gexfDocument.Meta.Creator = Environment.UserName;
            this.gexfDocument.Graph.IdType = GexfIdType.Integer;
            this.gexfDocument.Graph.Mode = GexfModeType.Static;
            this.gexfDocument.Graph.DefaultedEdgeType = GexfEdgeType.Directed;
        }
    }
}
