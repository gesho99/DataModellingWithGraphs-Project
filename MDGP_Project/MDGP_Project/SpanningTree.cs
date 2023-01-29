namespace MDGP_Project
{
    using Gexf;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SpanningTree
    {
        // Kruskal Algorithm - Maximum spanning tree
        public List<Tuple<string, string, GexfFloat>> GenerateSpanningTree(GexfDocument gexfDocument)
        {
            var edgesWithWeights = new Dictionary<GexfEdge, float>();

            foreach (var edge in gexfDocument.Graph.Edges)
            {
                edgesWithWeights.Add(edge, (float)edge.Weight);
            }

            // Sorting the dictionary
            edgesWithWeights = edgesWithWeights.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            var maximumSpanningTree = new List<Tuple<string, string, GexfFloat>>();
            var visitedNodes = new List<string>();
            var destinations = new List<string>();

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
            }

            return maximumSpanningTree;
        }
    }
}
