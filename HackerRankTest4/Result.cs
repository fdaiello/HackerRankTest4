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
        public static int cutTheTree(List<int> data, List<List<int>> edges)
        {

            return -1;
        }
        public static List<int> componentsInGraph(List<List<int>> gb)
        {
            return new List<int>();
        }

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
         */
        public static List<int> Bfs(int n, int m, List<List<int>> edges, int s)
        {

            int[] distances = new int[n + 1];
            for (int i = 0; i <= n; i++)
            {
                distances[i] = -1;
            }
            distances[0] = 0;
            distances[s] = 0;

            Queue<int[]> queue = new Queue<int[]>();
            queue.Enqueue(new int[] { s, 0 });


            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                foreach (List<int> edge in edges.Where(p => p[0] == node[0]))
                {
                    if (distances[edge[1]] == -1 )
                    {
                        queue.Enqueue(new int[] { edge[1], node[1] + 1 });
                        distances[edge[1]] = node[1] + 1;
                    }
                }
                foreach (List<int> edge in edges.Where(p => p[1] == node[0]))
                {
                    if (distances[edge[0]] == -1 )
                    {
                        queue.Enqueue(new int[] { edge[0], node[1] + 1 });
                        distances[edge[0]] = node[1] + 1;
                    }
                }
            }

            return distances.Where(p => p != 0).Select(p => p == -1 ? p : p * 6).ToList();

        }
        public static List<int> Bfs3(int n, int m, List<List<int>> edges, int s)
        {

            int[] distances = new int[n+1];
            for ( int i=0; i<=n; i++)
            {
                distances[i] = -1;
            }
            distances[0] = 0;
            distances[s] = 0;

            Queue<int[]> queue = new Queue<int[]>();
            queue.Enqueue(new int[] { s, 0 });


            while ( queue.Count > 0)
            {
                var node = queue.Dequeue();

                foreach ( List<int> edge in edges.Where(p => p[0] == node[0]))
                {
                    if (distances[edge[1]] == -1 || distances[edge[1]] > node[1]+1)
                    {
                        queue.Enqueue(new int[] { edge[1], node[1] + 1 });
                        distances[edge[1]] = node[1]+1;
                    }
                }
                foreach (List<int> edge in edges.Where(p => p[1] == node[0]))
                {
                    if (distances[edge[0]] == -1 || distances[edge[0]] > node[1] + 1)
                    {
                        queue.Enqueue(new int[] { edge[0], node[1] + 1 });
                        distances[edge[0]] = node[1] + 1;
                    }
                }
            }

            return distances.Where(p=>p!=0).Select(p=>p==-1?p:p*6).ToList();

        }
        public static List<int> Bfs2(int n, int m, List<List<int>> edges, int s)
        {

            // Map for nodes
            Dictionary<int, Node> map = new Dictionary<int, Node>();

            // Build graph
            for (int i = 0; i < edges.Count; i++)
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
                if (!node1.Edges.Contains(node2))
                    node1.Edges.Add(node2);
                if (!node2.Edges.Contains(node1))
                    node2.Edges.Add(node1);
            }

            int[] nodes = new int[n + 1];
            for ( int i=1; i<=n; i++)
                nodes[i] = -1;
            nodes[s] = 0;

            Queue<Tuple<Node, int>> q = new Queue<Tuple<Node, int>>();
            q.Enqueue(new Tuple<Node,int>(map[s],0));

            while ( q.Count > 0)
            {
                Tuple<Node,int> nq = q.Dequeue();


                foreach(Node child in nq.Item1.Edges)
                {
                    if (nodes[child.Value] == -1)
                    {
                        nodes[child.Value] = nq.Item2 + 1;
                        q.Enqueue(new Tuple<Node, int>(child, nq.Item2 + 1));
                    }
                }
            }

            // Return list with dephts
            return nodes.Where(p=>p!=0).Select(p=>p==-1?-1:p*6).ToList();
        }
        public static List<int> Bfs1(int n, int m, List<List<int>> edges, int s)
        {

            // Return list with depths
            List<int> rList = new List<int>();

            // Map for nodes
            Dictionary<int, Node> map = new Dictionary<int, Node>();

            // Build graph
            for (int i = 0; i < edges.Count; i++)
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
                if (!node1.Edges.Contains(node2))
                    node1.Edges.Add(node2);
                if (!node2.Edges.Contains(node1))
                    node2.Edges.Add(node1);
            }

            // Traverse all nodes
            for (int j = 1; j <= n; j++)
            {
                // - except start node
                if (j != s)
                {
                    // List of visited nodes
                    List<int> visited = new List<int>();

                    // start node
                    Node root = map[s];

                    // level count
                    int level = 0;

                    // found flag
                    bool found = false;

                    // Quee for each level
                    Queue<Tuple<Node,int>> queue = new Queue<Tuple<Node,int>>();

                    // Start inserting root node at Queue
                    Tuple<Node, int> tuple = new Tuple<Node, int>(root,level);
                    queue.Enqueue(tuple);

                    // While Queue is not empty
                    while ( queue.Any())
                    {
                        // Dequeue next node
                        tuple = queue.Dequeue();
                        Node node = tuple.Item1;

                        // Check node value - to see if we found who we are looking for
                        if ( node.Value == j)
                        {
                            found = true;
                            level = tuple.Item2;
                            break;
                        }
                        // Enqueue all child
                        foreach( Node child in node.Edges)
                        {
                            if (! visited.Contains(child.Value))
                            {
                                queue.Enqueue(new Tuple<Node, int>(child, tuple.Item2+1));
                                visited.Add(child.Value);
                            }
                        }
                    }

                    rList.Add(found?level*6:-1);
                }
            }


            // Return list with dephts
            return rList;
        }

        /*
         *  DEEP First Search: Shortest Reach:
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
         */
        public static List<int> DeepFirstSearch(int n, int m, List<List<int>> edges, int s)
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
            for ( int j=1; j<= n; j++)
            {
                // - except start node
                if ( j != s)
                {
                    // start node
                    Node pn = map[s];

                    // Get depth
                    int depth = 1;
                    List<int> visited = new List<int>();

                    if ( GetDepth(pn, j, ref depth, visited))
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

            visited.Add(node.Value);

            foreach ( Node chilld in node.Edges)
            {
                if ( chilld.Value == value)
                {
                    return true;
                }
                else if ( !visited.Contains(chilld.Value))
                {
                    if (GetDepth(chilld, value, ref depth, visited))
                    {
                        depth++;
                        return true;
                    }
                }
            }
            return false;

        }
    }
}
