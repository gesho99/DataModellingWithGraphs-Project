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

                //constructing the maximum spanning tree
                Dictionary<string, string> maximumSpanningTree = new Dictionary<string, string>();
                float lastWeight = 0.0f;
                bool toAddValue = false;
                foreach (var edge in edgesWithWeights.Keys)
                {
                    //Checking whether the weight is equal to the weight before, if yes -> skipping
                    if((float)edgesWithWeights.GetValueOrDefault(edge) == lastWeight)
                    {
                        continue;
                    }

                    //Here we should implement Deep First Search or Breadth First Search and to decide whether toAddValue or no

                    //Nor the source nor the target are in the spanning tree, so add them
                    if (toAddValue == true)
                    {
                        try
                        {
                            maximumSpanningTree.Add(edge.Source.ToString(), edge.Target.ToString());
                        }
                        catch (System.ArgumentException e)
                        {
                            maximumSpanningTree.Add(edge.Target.ToString(), edge.Source.ToString());
                        }
                    }

                    lastWeight = (float)edge.Weight;
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
    }
}