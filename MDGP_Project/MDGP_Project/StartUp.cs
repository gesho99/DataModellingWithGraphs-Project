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
            var graph = new Dictionary<string, List<string>>();

            // Load the workbook.
            SpreadsheetGear.IWorkbook workbook = SpreadsheetGear.Factory.GetWorkbook(@"mtx_correl_log_ret.csv");
            // Get a range of cells as an array of object[,].
            object[,] values = (object[,])workbook.Worksheets["in"].Cells["A1:Z26"].Value;
            int[] whiteList = new int[3];

            decimal firstLargestNum = 0, secondLargestNum = 0, thirdLargestNum = 0;

            for (int row = 1; row < values.GetLength(0); row++)
            {
                for (int col = 1; col < values.GetLength(0); col++)
                {
                    decimal value = decimal.Parse(values[row, col].ToString());
                    if (value > thirdLargestNum)
                    {
                        if (value > secondLargestNum)
                        {
                            if (value > firstLargestNum)
                            {
                                thirdLargestNum = secondLargestNum;
                                secondLargestNum = firstLargestNum;
                                firstLargestNum = value;
                                whiteList[0] = col;
                            }
                            else
                            {
                                thirdLargestNum = secondLargestNum;
                                secondLargestNum = value;
                                whiteList[1] = col;
                            }
                        }
                        else
                        {
                            thirdLargestNum = value;
                            whiteList[2] = col;
                        }
                    }
                }

                List<string> companiesWithLargestNums = new List<string>();
                companiesWithLargestNums.Add(values[0, whiteList[0]].ToString());
                companiesWithLargestNums.Add(values[0, whiteList[1]].ToString());
                companiesWithLargestNums.Add(values[0, whiteList[2]].ToString());

                graph.Add(values[row, 0].ToString(), companiesWithLargestNums);
                
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

           /* var counter = 1;
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
            }*/

            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "graph.gexf");

            gexfModel.Save(path);
        }
    }
}