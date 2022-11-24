using Gexf;
using Gexf.Visualization;
using System;
using System.Numerics;

namespace MDGP_Project
{
    internal class Program
    {
        static List<int> factors = new List<int>();
        static Dictionary<int, string> nodeColors = new Dictionary<int, string>();
        static Dictionary<int, List<int>> graph = new Dictionary<int, List<int>>();

        static void Main(string[] args)
        {
            Console.Write("Enter the number of nodes: ");
            int nodes = int.Parse(Console.ReadLine());

            for(int i = 2; i <= nodes; i++)
            {
                if(isPrime(i))
                {
                    nodeColors.Add(i, "<viz:color r=\"0\" g=\"255\" b=\"0\"/>");
                    graph.Add(i, null);
                }
                else
                {
                    nodeColors.Add(i, "<viz:color r=\"165\" g=\"42\" b=\"42\"/>");
                    CalculatePrimeFactors(i);
                    graph.Add(i, factors);
                }
            }

            var gexf = new GexfDocument();
            gexf.Meta.LastModified = DateTimeOffset.Now;
            gexf.Meta.Creator = Environment.UserName;
            gexf.Graph.IdType = GexfIdType.Integer;

            gexf.Graph.Nodes.AddRange(graph.Keys.Select(key =>
            new GexfNode(key)
            {
                Label = key.ToString()
            }));

            int counter = 1;

            foreach (int node in graph.Keys)
            {
                if (!(graph[node] is null))
                {
                    foreach (int connection in graph[node])
                    {
                        gexf.Graph.Edges.AddRange(
                        new GexfEdge(counter, node, connection)
                        );

                        counter++;
                    }
                }
            }

            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "breweries.gexf");

            gexf.Save(path);
        }

        static void CalculatePrimeFactors(int number)
        {
            factors = new List<int>();

            for (int divisor = 2; divisor <= (int)(number / 2); divisor++)
            {
                if(number % divisor == 0)
                {
                    if (isPrime(divisor))
                    {
                        factors.Add(divisor);
                    }
                }
            }
        }

        static bool isPrime(int number)
        {
            if (number == 1) return false;
            if (number == 2) return true;

            var limit = Math.Ceiling(Math.Sqrt(number)); //hoisting the loop limit

            for (int i = 2; i <= limit; ++i)
                if (number % i == 0)
                    return false;
            return true;

        }
    }
}