namespace MDGP_Project
{
    using System.Drawing;
    using System.Numerics;
    using Gexf;
    using Microsoft.VisualBasic.FileIO;

    public class StartUp
    {
        public static void Main()
        {
            var graph = new Dictionary<string, List<string>>();
            var edges = new Dictionary<int, List<float>>();

            // Load the workbook.
            SpreadsheetGear.IWorkbook workbook = SpreadsheetGear.Factory.GetWorkbook(@"mtx_correl_log_ret.csv");
            // Get a range of cells as an array of object[,].
            object[,] values = (object[,])workbook.Worksheets["in"].Cells["A1:Z26"].Value;
            int[] whiteList = new int[3];

            float firstLargestNum = 0, secondLargestNum = 0, thirdLargestNum = 0;

            int counter = 1;
            for (int row = 1; row < values.GetLength(0); row++)
            {
                for (int col = 1; col < values.GetLength(0); col++)
                {
                    float value = float.Parse(values[row, col].ToString());
                    if (value > thirdLargestNum)
                    {
                        if (value > secondLargestNum)
                        {
                            if (value > firstLargestNum)
                            {
                                thirdLargestNum = secondLargestNum;
                                secondLargestNum = firstLargestNum;
                                firstLargestNum = value;
                                whiteList[2] = whiteList[1];
                                whiteList[1] = whiteList[0];
                                whiteList[0] = col;
                            }
                            else
                            {
                                thirdLargestNum = secondLargestNum;
                                secondLargestNum = value;
                                whiteList[2] = whiteList[1];
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
                edges.Add(counter, new List<float> { firstLargestNum, secondLargestNum, thirdLargestNum });

                counter++;

                firstLargestNum = 0;
                secondLargestNum = 0;
                thirdLargestNum = 0;
            }

            var gexfDocument = new GexfDocument();
            var gexfModel = new GexfModel(gexfDocument);

            var nodes = graph.Keys
                .Select(key =>
                    new GexfNode(key)
                    {
                        Label = key
                    })
                .ToList();

            gexfModel.AddNodes(nodes);
            //gexfModel.SetNodesColors(Color.Green, Color.Red);

            int counterForEdgeID = 0;
            counter = 1;
            foreach (string node in graph.Keys)
            {
                List<string> nodeValues = graph.GetValueOrDefault(node);
                int counterForEdgesValues = 0;
                foreach (string nodeValue in nodeValues)
                {
                    GexfEdge edge = new GexfEdge(counterForEdgeID, node, nodeValue);
                    edge.Weight = edges.GetValueOrDefault(counter)[counterForEdgesValues];
                    Type test = edge.Weight.GetType();
                    gexfModel.AddEdges(edge);

                    counterForEdgeID++;
                    counterForEdgesValues++;
                }
                counter++;
            }

            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "graph.gexf");

            gexfModel.Save(path);
        }
    }
}