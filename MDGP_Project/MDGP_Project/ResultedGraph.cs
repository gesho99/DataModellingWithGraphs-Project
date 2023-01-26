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

            var whiteList = new int[3];

            float firstLargestNum = 0;
            float secondLargestNum = 0;
            float thirdLargestNum = 0;

            var counter = 1;
            for (var row = 1; row < values.GetLength(0); row++)
            {
                for (var col = 1; col < values.GetLength(1); col++)
                {
                    // Take value from matrix and parse it to float
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

                // Remove duplicated connections and return modified list with largest nums
                companiesWithLargestNums = this.RemoveDuplicatedConnections(
                    companiesWithLargestNums,
                    graph,
                    firstLargestNum,
                    secondLargestNum,
                    thirdLargestNum,
                    values,
                    row);

                graph.Add(values[row, 0]?.ToString(), companiesWithLargestNums);

                // Checking which connection we removed in order to reconstruct the edges
                this.ReconstructEdges(edges, firstLargestNum, secondLargestNum, thirdLargestNum, counter);

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

        private void ReconstructEdges(
            Dictionary<int, List<float>> edges,
            float firstLargestNum,
            float secondLargestNum,
            float thirdLargestNum,
            int counter)
        {
            if (firstLargestNum != 0 && secondLargestNum != 0 && thirdLargestNum != 0)
            {
                edges.Add(counter, new List<float> { firstLargestNum, secondLargestNum, thirdLargestNum });
            }
            else if (firstLargestNum == 0 && secondLargestNum != 0 && thirdLargestNum != 0)
            {
                edges.Add(counter, new List<float> { secondLargestNum, thirdLargestNum });
            }
            else if (secondLargestNum == 0 && firstLargestNum != 0 && thirdLargestNum != 0)
            {
                edges.Add(counter, new List<float> { firstLargestNum, thirdLargestNum });
            }
            else if (thirdLargestNum == 0 && firstLargestNum != 0 && secondLargestNum != 0)
            {
                edges.Add(counter, new List<float> { firstLargestNum, secondLargestNum });
            }
            else if (firstLargestNum != 0)
            {
                edges.Add(counter, new List<float> { firstLargestNum });
            }
            else if (secondLargestNum != 0)
            {
                edges.Add(counter, new List<float> { secondLargestNum });
            }
            else if (thirdLargestNum != 0)
            {
                edges.Add(counter, new List<float> { thirdLargestNum });
            }
        }

        private List<string> RemoveDuplicatedConnections(
            List<string> companiesWithLargestNums,
            Dictionary<string, List<string>> graph,
            float firstLargestNum,
            float secondLargestNum,
            float thirdLargestNum,
            object[,] values,
            int row)
        {
            var valuesToRemove = new List<string>();
            foreach (var node in companiesWithLargestNums)
            {
                if (graph.GetValueOrDefault(node) != null)
                {
                    foreach (var dsa in graph.GetValueOrDefault(node))
                    {
                        // Ff there is already such connection between two nodes, do not add it again
                        if (dsa.Contains(values[row, 0].ToString()))
                        {
                            valuesToRemove.Add(node);
                        }
                    }
                }
            }

            // Remove the connection that already exists in the graph
            foreach (var element in valuesToRemove)
            {
                var index = companiesWithLargestNums.IndexOf(element);

                if (index == 0)
                {
                    firstLargestNum = 0;
                }
                else if (index == 1)
                {
                    secondLargestNum = 0;
                }
                else if (index == 2)
                {
                    thirdLargestNum = 0;
                }
            }

            valuesToRemove.ForEach(value => companiesWithLargestNums.Remove(value));

            return companiesWithLargestNums;
        }
    }
}
