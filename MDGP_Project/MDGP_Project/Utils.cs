namespace MDGP_Project
{
    using Gexf;

    public static class Utils
    {
        // Recursive function to get all the direct and indirect destinations of a node
        public static void GetAllDestinationDestinations(
            List<Tuple<string, string, GexfFloat>> maximumSpanningTree,
            string node,
            List<string> destinations)
        {
            // Getting all the connections of the node
            var edges = maximumSpanningTree.FindAll(nd => nd.Item1 == node || nd.Item2 == node);

            // If there are no connections we return; Bottom of recursion
            if (edges.Count == 0)
            {
                return;
            }

            // Foreach connection
            foreach (var edge in edges)
            {
                // If the node is on the right side of the connection
                if (edge.Item1 == node)
                {
                    if (destinations.Contains(edge.Item2))
                    {
                        continue;
                    }

                    destinations.Add(edge.Item2);

                    // Get the connections of the node's destination
                    GetAllDestinationDestinations(maximumSpanningTree, edge.Item2, destinations);
                }
                // If the node is on the left side of the connection
                else if (edge.Item2 == node)
                {
                    if (destinations.Contains(edge.Item1))
                    {
                        continue;
                    }

                    destinations.Add(edge.Item1);

                    //Get the connections of the node's destination
                    GetAllDestinationDestinations(maximumSpanningTree, edge.Item1, destinations);
                }
            }
        }

        public static void SaveGraph(string prefix, string workbookName, GexfModel gexfModel)
        {
            var path = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    $"{prefix}_{workbookName.Replace(".csv", "")}.gexf");

            gexfModel.Save(path);
        }
    }
}
