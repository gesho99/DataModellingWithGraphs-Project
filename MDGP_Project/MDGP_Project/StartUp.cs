namespace MDGP_Project
{
    using Gexf;
    using SpreadsheetGear;
    using System.ComponentModel;
    using System.Diagnostics.Metrics;
    using System.Linq;
    using System.Xml.Linq;


    public class StartUp
    {
        public static void Main()
        {
            // Load the workbooks
            var workbooks = new List<IWorkbook>
            {
                Factory.GetWorkbook(@"mtx_correl_log_ret.csv"),
                Factory.GetWorkbook(@"mtx_correl_ewm_vol.csv")
            };

            // Process given workbooks
            foreach (var workbook in workbooks)
            {
                // Get values from the workbooks
                var values = (object[,])workbook.Worksheets[0].Cells["A1:Z26"].Value;

                // Execute algorithm on values from the workbooks
                var resultedGraph = new ResultedGraph();
                resultedGraph = resultedGraph.RunAlgorithm(values);

                // Make visualization of each graph
                var gexfDocument = new GexfDocument();
                var gexfModel = new GexfModel(gexfDocument);
                gexfModel = gexfModel.ProcessVisualization(gexfModel, resultedGraph);

                // Save each graph on separate GEXF file
                var path = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    $"graph_{workbook.Name.Replace(".csv", "")}.gexf");

                gexfModel.Save(path);

                // Kruskal Algorithm - Maximum spanning tree (Check for loops)
                Dictionary<GexfEdge, float> edgesWithWeights = new Dictionary<GexfEdge, float>();
                foreach(var edge in gexfDocument.Graph.Edges)
                {
                    edgesWithWeights.Add(edge, (float)edge.Weight);
                }

                //sorting the dictionary
                List<KeyValuePair<GexfEdge, float>> sortedEdgesWithWeights = edgesWithWeights.ToList();
                sortedEdgesWithWeights.Sort(
                    delegate (KeyValuePair<GexfEdge, float> pair1,
                    KeyValuePair<GexfEdge, float> pair2)
                    {
                        return pair1.Value.CompareTo(pair2.Value);
                    }
                );

                edgesWithWeights = sortedEdgesWithWeights.ToDictionary(x => x.Key, x => x.Value);

                List<Tuple<string, string>> maximumSpanningTree = new List<Tuple<string, string>>();
                List<string> visitedNodes = new List<string>();
                bool toAddValue = false;
                List<string> destinations = new List<string>();

                int counterForDebbuging = 0;
                foreach (var edge in edgesWithWeights.Keys)
                {             
                    //Checking whether one of the elements is already visited
                    if((!visitedNodes.Contains(edge.Source.ToString())) && visitedNodes.Contains(edge.Target.ToString()))
                    {
                        maximumSpanningTree.Add(new Tuple<string, string>(edge.Target.ToString(), edge.Source.ToString()));
                        visitedNodes.Add(edge.Source.ToString());
                    }
                    else if((!visitedNodes.Contains(edge.Target.ToString())) && visitedNodes.Contains(edge.Source.ToString()))
                    {
                        maximumSpanningTree.Add(new Tuple<string, string>(edge.Source.ToString(), edge.Target.ToString()));
                        visitedNodes.Add(edge.Target.ToString());
                    }
                    else if((!visitedNodes.Contains(edge.Source.ToString())) && (!visitedNodes.Contains(edge.Target.ToString())))
                    {
                        maximumSpanningTree.Add(new Tuple<string, string>(edge.Source.ToString(), edge.Target.ToString()));
                        visitedNodes.Add(edge.Source.ToString());
                        visitedNodes.Add(edge.Target.ToString());
                    }
                    //Checking if both of the elemenst are alredy visited - if yes - are they connected to eachother through other nodes?
                    else
                    {
                        GetAllDestinationDestinations(maximumSpanningTree, edge.Source.ToString(), destinations);

                        if(!destinations.Contains(edge.Target.ToString()))
                        {
                            maximumSpanningTree.Add(new Tuple<string, string>(edge.Source.ToString(), edge.Target.ToString()));
                        }

                        destinations.Clear();
                    }

                    counterForDebbuging++;
                }

                gexfDocument = new GexfDocument();
                gexfModel = new GexfModel(gexfDocument);

                gexfModel = gexfModel.ProcessVisualization(gexfModel, maximumSpanningTree, edgesWithWeights);

                // Save each maximum spanning tree on separate GEXF file
                path = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    $"maximum_spanning_tree_{workbook.Name.Replace(".csv", "")}.gexf");

                gexfModel.Save(path);
            }
        }

        //Recursive function to get all the direct and indirect destinations of a node
        static void GetAllDestinationDestinations(List<Tuple<string, string>> maximumSpanningTree, string node, List<string> destinations)
        {
            //Getting all the connections of the node
            List<Tuple<string, string>> edges = maximumSpanningTree.FindAll(nd => nd.Item1 == node || nd.Item2 == node);

            //If there are no connections we return
            if(edges.Count == 0)
            {
                return;
            }

            //Foreach connection
            foreach(var edge in edges)
            {
                //If the node is on the right side of the connection
                if(edge.Item1 == node)
                {
                    if(destinations.Contains(edge.Item2))
                    {
                        continue;
                    }
                    destinations.Add(edge.Item2);
                    //Get the connections of the node's destination
                    GetAllDestinationDestinations(maximumSpanningTree, edge.Item2, destinations);
                }
                //If the node is on the left side of the connection
                else if(edge.Item2 == node)
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
    }
}