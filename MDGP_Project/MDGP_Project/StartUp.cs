namespace MDGP_Project
{
    public class StartUp
    {
        static void Main()
        {
            Console.Write("Enter the number of nodes: ");
            var nodesCount = int.Parse(Console.ReadLine());
            var graph = new Dictionary<int, List<int>>();
            //var nodeColors = new Dictionary<int, string>();

            for (int i = 2; i <= nodesCount; i++)
            {
                if (Utils.IsPrime(i))
                {
                    //nodeColors.Add(i, Constants.PrimeColor);
                    graph.Add(i, new List<int>());
                }
                else
                {
                    // nodeColors.Add(i, Constants.NonPrimeColor);
                    var factors = Utils.CalculatePrimeFactors(i);
                    graph.Add(i, factors);
                }
            }

            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "breweries.gexf");

            Utils.GenerateVisualization(graph, path);
        }
    }
}