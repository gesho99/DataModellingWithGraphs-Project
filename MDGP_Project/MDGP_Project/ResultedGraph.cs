namespace MDGP_Project
{
    public class ResultedGraph
    {
        public ResultedGraph()
        {
            this.Graph = new Dictionary<string, List<string>>();
            this.Edges = new Dictionary<int, List<float>>();
        }

        public Dictionary<string, List<string>> Graph { get; private set; }

        public Dictionary<int, List<float>> Edges { get; private set; }

        public ResultedGraph RunAlgorithm(object[,] values)
        {
            var graph = new Dictionary<string, List<string>>();
            var edges = new Dictionary<int, List<float>>();

            int[] whiteList = new int[3];

            float firstLargestNum = 0;
            float secondLargestNum = 0;
            float thirdLargestNum = 0;

            var counter = 1;
            for (var row = 1; row < values.GetLength(0); row++)
            {
                for (var col = 1; col < values.GetLength(0); col++)
                {
                    var value = float.Parse(values[row, col].ToString());

                    // Skip the iteration if value is 1 (this is the diagonal value)
                    if (value == 1)
                    {
                        continue;
                    }

                    // Calculate the first/second/third largest number
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

                var companiesWithLargestNums = new List<string>
                {
                    values[0, whiteList[0]].ToString(),
                    values[0, whiteList[1]].ToString(),
                    values[0, whiteList[2]].ToString()
                };

                graph.Add(values[row, 0].ToString(), companiesWithLargestNums);
                edges.Add(counter, new List<float> { firstLargestNum, secondLargestNum, thirdLargestNum });

                counter++;

                firstLargestNum = 0;
                secondLargestNum = 0;
                thirdLargestNum = 0;
            }

            var resultedGraph = new ResultedGraph
            {
                Graph = graph,
                Edges = edges
            };

            return resultedGraph;
        }
    }
}
