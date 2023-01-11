namespace MDGP_Project
{
    using System.Drawing;
    using Gexf;
    using Microsoft.VisualBasic.FileIO;

    public class StartUp
    {
        public static void Main()
        {
            Console.Write("Enter the number of nodes: ");
            var nodesCount = int.Parse(Console.ReadLine());
            var graph = new Dictionary<int, List<int>>();

            // Load the workbook.
            SpreadsheetGear.IWorkbook workbook = SpreadsheetGear.Factory.GetWorkbook(@"mtx_correl_log_ret.csv");
            // Get a range of cells as an array of object[,].
            object[,] values = (object[,])workbook.Worksheets["in"].Cells["A1:Z26"].Value;

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

            var counter = 1;
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
                "graph.gexf");

            gexfModel.Save(path);
        }
    }
}