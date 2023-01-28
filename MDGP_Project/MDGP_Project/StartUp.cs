namespace MDGP_Project
{
    using Gexf;
    using SpreadsheetGear;

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

                    // Generate maximum spanning tree
                    var spanningTree = new SpanningTree();
                    spanningTree = spanningTree.GenerateSpanningTree(gexfDocument);

                    gexfDocument = new GexfDocument();
                    gexfModel = new GexfModel(gexfDocument);

                    gexfModel = gexfModel.ProcessVisualization(gexfModel, spanningTree.MaximumSpanningTree, spanningTree.EdgesWithWeights);

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