using Gexf;
using Gexf.Visualization;
using System.Drawing;

namespace MDGP_Project
{
    public static class Utils
    {
        public static List<int> CalculatePrimeFactors(int number)
        {
            var factors = new List<int>();

            for (int divisor = 2; divisor <= number / 2; divisor++)
            {
                if (number % divisor == 0)
                {
                    if (IsPrime(divisor))
                    {
                        factors.Add(divisor);
                    }
                }
            }

            return factors;
        }

        // Is this function ok, I saw in the net that the for loop starts from 3
        public static bool IsPrime(int number)
        {
            if (number == 1)
            {
                return false;
            }

            if (number == 2)
            {
                return true;
            }

            var limit = Math.Ceiling(Math.Sqrt(number)); //hoisting the loop limit

            for (int i = 2; i <= limit; ++i)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

        public static void GenerateVisualization(Dictionary<int, List<int>> graph, string path)
        {
            var gexf = new GexfDocument();
            gexf.Meta.LastModified = DateTimeOffset.Now;
            gexf.Meta.Creator = Environment.UserName;
            gexf.Graph.IdType = GexfIdType.Integer;
            gexf.Graph.Mode = GexfModeType.Static;
            gexf.Graph.DefaultedEdgeType = GexfEdgeType.Directed;

            gexf.Graph.Nodes.AddRange(graph.Keys.Select(key =>
                new GexfNode(key)
                {
                    Label = key.ToString()
                }));

            foreach (var item in gexf.Graph.Nodes)
            {
                if (IsPrime(int.Parse(item.Id.ToString())))
                {
                    item.Color(Color.Green);
                }
                else
                {
                    item.Color(Color.Red);
                }
            }

            int counter = 1;

            foreach (int node in graph.Keys)
            {
                if (graph[node] != null)
                {
                    foreach (int connection in graph[node])
                    {
                        gexf.Graph.Edges.AddRange(new GexfEdge(counter, node, connection));

                        counter++;
                    }
                }
            }

            gexf.Save(path);
        }
    }
}
