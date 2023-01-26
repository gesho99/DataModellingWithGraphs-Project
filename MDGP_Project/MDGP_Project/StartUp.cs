namespace MDGP_Project
{
    using Gexf;
    using SpreadsheetGear;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            try
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

                    // Save each graph on separate GEXF file; point 4 and 5 from project requirements
                    Utils.SaveGraph("graph", workbook.Name, gexfModel);

                    // Kruskal Algorithm - Maximum spanning tree
                    var edgesWithWeights = new Dictionary<GexfEdge, float>();
                    foreach (var edge in gexfDocument.Graph.Edges)
                    {
                        edgesWithWeights.Add(edge, (float)edge.Weight);
                    }

                    // Sorting the dictionary
                    edgesWithWeights = edgesWithWeights.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

                    var maximumSpanningTree = new List<Tuple<string, string, GexfFloat>>();
                    var visitedNodes = new List<string>();
                    var destinations = new List<string>();

                    // var counterForDebbuging = 0;
                    foreach (var edge in edgesWithWeights.Keys)
                    {
                        // Checking whether one of the elements is already visited
                        if ((!visitedNodes.Contains(edge.Source.ToString())) && visitedNodes.Contains(edge.Target.ToString()))
                        {
                            maximumSpanningTree.Add(new Tuple<string, string, GexfFloat>(edge.Target.ToString(), edge.Source.ToString(), edge.Weight));
                            visitedNodes.Add(edge.Source.ToString());
                        }
                        else if ((!visitedNodes.Contains(edge.Target.ToString())) && visitedNodes.Contains(edge.Source.ToString()))
                        {
                            maximumSpanningTree.Add(new Tuple<string, string, GexfFloat>(edge.Source.ToString(), edge.Target.ToString(), edge.Weight));
                            visitedNodes.Add(edge.Target.ToString());
                        }
                        else if ((!visitedNodes.Contains(edge.Source.ToString())) && (!visitedNodes.Contains(edge.Target.ToString())))
                        {
                            maximumSpanningTree.Add(new Tuple<string, string, GexfFloat>(edge.Source.ToString(), edge.Target.ToString(), edge.Weight));
                            visitedNodes.Add(edge.Source.ToString());
                            visitedNodes.Add(edge.Target.ToString());
                        }
                        // Checking if both of the elements are already visited - if yes - are they connected to each other through other nodes?
                        else
                        {
                            Utils.GetAllDestinationDestinations(maximumSpanningTree, edge.Source.ToString(), destinations);

                            if (!destinations.Contains(edge.Target.ToString()))
                            {
                                maximumSpanningTree.Add(new Tuple<string, string, GexfFloat>(edge.Source.ToString(), edge.Target.ToString(), edge.Weight));
                            }

                            destinations.Clear();
                        }

                        // counterForDebbuging++; do we need that?
                    }

                    gexfDocument = new GexfDocument();
                    gexfModel = new GexfModel(gexfDocument);

                    gexfModel = gexfModel.ProcessVisualization(gexfModel, maximumSpanningTree, edgesWithWeights);

                    // Save each maximum spanning tree on separate GEXF file; point 6 and 7 from project requirements
                    Utils.SaveGraph("maximum_spanning_tree", workbook.Name, gexfModel);
                }

                Console.WriteLine("The requsted 4 graphs are successfully exported!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}