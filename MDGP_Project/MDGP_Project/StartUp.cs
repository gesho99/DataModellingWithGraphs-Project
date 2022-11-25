namespace MDGP_Project
{
    using System.Drawing;
    using Gexf;

    public class StartUp
    {
        static void Main()
        {
            Console.Write("Enter the number of nodes: ");
            var nodesCount = int.Parse(Console.ReadLine());
            // TODO: Create class for graph instead of that dictionary
            var graph = new Dictionary<int, List<int>>();

            for (int i = 2; i <= nodesCount; i++)
            {
                if (Utils.IsPrime(i))
                {
                    graph.Add(i, new List<int>());
                }
                else
                {
                    var factors = Utils.CalculatePrimeFactors(i);
                    graph.Add(i, factors);
                }
            }

            var gexfDocument = new GexfDocument();
            var gexfModel = new GexfModel(gexfDocument);
            var nodes = graph.Keys
                .Select(key =>
                    new GexfNode(key)
                    {
                        Label = key.ToString()
                    })
                .ToList();

            gexfModel.AddNodes(nodes);
            gexfModel.SetNodesColors(Color.Green, Color.Red);

            int counter = 1;
            foreach (int node in graph.Keys)
            {
                if (graph[node] != null)
                {
                    foreach (int connection in graph[node])
                    {
                        gexfModel.AddEdges(new GexfEdge(counter, node, connection));

                        counter++;
                    }
                }
            }

            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "breweries.gexf");

            gexfModel.Save(path);
        }
    }
}