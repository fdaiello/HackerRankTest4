using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRankTest4
{
    class Node
    {
        public Node(int value)
        {
            Value = value;
            Edges = new List<Node>();
        }
        public int Value { get; set; }
        public List<Node> Edges { get; set; }
    }
    class Result
    {
       /*
        *  Beadth First Search: Shortest Reach:
        *  
        *  INPUT
        *  n - number of nodes
        *  m - number of vertices
        *  edges - list of edges ( pairs of nodes that are conected )
        *  s - starting node
        *  
        *  Obs: Consider each edge weight 6 units
        *  
        *  OUTPUT
        *  List with the distance of all nodes to starting node - not considering starting node
        *  
        *  
        *  
        *  
        *  BUGS: 
        *        1) dept counter
        *        2) Not checking all nodes ( its checking only nodes in map, that is, in graph )
        *  
        */

        public static List<int> bfs(int n, int m, List<List<int>> edges, int s)
        {

            // Return list with depths
            List<int> rList = new List<int>();

            // Map for nodes
            Dictionary<int, Node> map = new Dictionary<int, Node>();

            // Build graph
            for ( int i =0; i < edges.Count; i++)
            {
                int n1Value = edges[i][0];
                int n2Value = edges[i][1];

                // Are nodes already in graph?
                Node node1;
                if (map.ContainsKey(n1Value))
                {
                    node1 = map[n1Value];
                }
                else
                {
                    node1 = new Node(n1Value);
                    map.Add(n1Value, node1);
                }
                Node node2;
                if (map.ContainsKey(n2Value))
                {
                    node2 = map[n2Value];
                }
                else
                {
                    node2 = new Node(n2Value);
                    map.Add(n2Value, node2);
                }

                // Link nodes - create edge
                if ( ! node1.Edges.Contains(node2))
                    node1.Edges.Add(node2);
                if ( ! node2.Edges.Contains(node1))
                    node2.Edges.Add(node1);
            }

            // Traverse all nodes
            foreach ( KeyValuePair<int, Node> kv in map)
            {
                // - except start node
                if ( kv.Key != s)
                {
                    // start node
                    Node pn = map[s];

                    // Get depth
                    int depth = 0;
                    List<int> visited = new List<int>();

                    if ( GetDepth(pn, kv.Key, ref depth, visited))
                    {
                        rList.Add(depth);
                    }
                    else
                    {
                        rList.Add(-1);
                    }

                }
            }


            // Return list with dephts
            return rList;
        }
        static bool GetDepth(Node node, int value, ref int depth, List<int> visited)
        {
            depth++;

            if ( node.Value == value)
            {
                return true;
            }
            else
            {
                foreach ( Node chilld in node.Edges)
                {
                    if ( !visited.Contains(chilld.Value))
                    {
                        visited.Add(chilld.Value);
                        if (GetDepth(chilld, value, ref depth, visited))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }
    }
}
