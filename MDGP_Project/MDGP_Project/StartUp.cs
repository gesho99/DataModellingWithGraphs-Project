namespace MDGP_Project
{
    using Gexf;
    using SpreadsheetGear;

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
                object[,] values = (object[,])workbook.Worksheets[0].Cells["A1:Z26"].Value;

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
            }
        }
    }
}