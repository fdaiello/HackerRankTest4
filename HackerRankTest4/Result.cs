using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRankTest4
{
    class Result
    {
        #region CutTheTree
        /*
         *  Cut the Tree
         *  
         *  There is an undirected tree where each vertex is numbered from 1 to n, and each contains a data value.
         *  The sum of a tree is the sum of all its nodes' data values.
         *  If an edge is cut, two smaller trees are formed.
         *  The difference between two trees is the absolute value of the difference in their sums.
         *  
         *  Given a tree, determine wich edge to cut so that the resulting trees have a minimal difference between them,
         *  Then return that difference.
         *  
         */

        // Deep First Search from root ( first node )
        // Array with recursive cumulative sum for each node and its children
        // This is the optimized solution!
        public static int cutTheTree(List<int> data, List<List<int>> edges)
        {
            // Build adjancency list for graph
            List<int>[] graph = new List<int>[data.Count];
            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<int>();
            }

            foreach (var edge in edges)
            {
                graph[edge[0] - 1].Add(edge[1]);
                graph[edge[1] - 1].Add(edge[0]);
            }

            // Root node - assume its the first node from first edge
            int root = edges[0][0];

            // Get total sum of all tree
            int tSum = data.Sum();

            // min differece found up to each node
            int minDiff = int.MaxValue;

            // array with cumulative sum of each node and children value
            int[] sum = new int[data.Count];

            // start Deep First Search from first node - assume this is the root and has no parent
            Dfs(graph, data, root, -1, tSum, ref sum, ref minDiff);

            // Return compared minDiff for each step
            return minDiff;
        }

        // Recursive Deep First Search from given node
        // This algorithim didn't work. It just traverse summing values but cannot consider subtrees correctly
        public static void Dfs ( List<int>[] graph, List<int> data, int start, int parent, int tSum, ref int[] sum, ref int minDiff)
        {
            // Initialize the sum of the tree begining at this node with this node value
            sum[start - 1] = data[start - 1];

            // for all connected nodes
            foreach( int node in graph[start - 1])
            {
                // Excet for its own parent
                if ( node != parent)
                {
                    // Recursive call DFS for given node
                    Dfs(graph, data, node, start, tSum, ref sum, ref minDiff);

                    // And acumulate child value to parent value
                    sum[start - 1] += sum[node-1];
                }
            }

            // Check sum value for the subtree starting at this node: sum[start-1]
            // Get the absolute difference between this subtree and the rest. 
            // Compare and save minDiff
            minDiff = Math.Min(minDiff, Math.Abs(sum[start - 1] - (tSum - sum[start - 1])));

        }

        // Brute force approach - works but takes great time complexity
        //   cutTheTree1
        //       SumTree ( for each cutted edge )
        public static int cutTheTree1(List<int> data, List<List<int>> edges)
        {
            // Build adjancency list for graph
            List<int>[] graph = new List<int>[data.Count];
            for (int i = 0; i < graph.Length; i++)
            {
                graph[i] = new List<int>();
            }

            foreach (var edge in edges)
            {
                graph[edge[0] - 1].Add(edge[1]);
                graph[edge[1] - 1].Add(edge[0]);
            }

            // Get total sum of all tree
            int tSum = data.Sum();

            // Save min difference
            int minDiff = int.MaxValue;

            // For each edge
            foreach(var edge in edges)
            {
                // Get sum of tree without this edge
                int thisSum = SumTree(data, graph, edge, edge[0]);

                // Update minDiff
                minDiff = Math.Min(minDiff, Math.Abs(thisSum - (tSum-thisSum)));

            }


            // Return min Difference
            return minDiff;
        }
        public static int SumTree(List<int> data, List<int>[] graph, List<int> cuttedEdge, int start)
        {
            // Traverse graph from start, excluding cuttedNode
            Stack<int> s = new Stack<int>();
            HashSet<int> visited = new HashSet<int>();
            s.Push(start);
            visited.Add(start);
            int sum = 0;

            while (s.Count > 0)
            {
                int node = s.Pop();
                sum += data[node-1];

                foreach (int neighboor in graph[node - 1])
                {
                    if (cuttedEdge[0]!=node || cuttedEdge[1] != neighboor)
                    {
                        if (!visited.Contains(neighboor))
                        {
                            s.Push(neighboor);
                            visited.Add(neighboor);
                        }
                    }
                }
            }

            return sum;
        }

        // This was just an encapsulating call made to test the algorithim gotten from Geeks for Geeks.
        public static int cutTheTree3(List<int> data, List<List<int>> edges)
        {
            int[,] e = new int[edges.Count, 2];

            for (int i = 0; i < edges.Count; i++)
            {
                e[i, 0] = edges[i][0]-1;
                e[i, 1] = edges[i][1]-1;
            }

            // Geeks for Geeks ( aashish1995 ) algorithim.
            return Graph.getMinSubtreeSumDifference(data.ToArray(), e, data.Count);
        }

        // This was my first Trial. Works just in some cases. Cannot find subtrees correctly
        public static int cutTheTree2(List<int> data, List<List<int>> edges)
        {
            // Build adjancency list for graph
            List<int>[] graph = new List<int>[data.Count];
            for ( int i =0; i<graph.Length; i++)
            {
                graph[i] = new List<int>();
            }

            foreach ( var edge in edges)
            {
                graph[edge[0]-1].Add(edge[1]);
                graph[edge[1]-1].Add(edge[0]);
            }

            // Get any leaf
            int leaf = graph.Where(p => p.Count == 1).FirstOrDefault()[0]+1;

            // Get total sum of all tree
            int tSum = data.Sum();
            int sum1 = 0;
            int sum2 = 0;

            // Traverse from leaf summing values
            Stack<int> s = new Stack<int>();
            HashSet<int> visited = new HashSet<int>();
            s.Push(leaf);
            visited.Add(leaf);

            while ( s.Count > 0)
            {
                int node = s.Pop();

                // Compare acumulated sum to total sum of tree
                sum1 += data[node - 1];
                if ( sum1 >= tSum / 2)
                {
                    return Math.Abs(Math.Min(sum1 - tSum / 2, sum2 - tSum / 2));
                }
                sum2 = sum1;

                foreach ( int neighboor in graph[node-1])
                {
                    if (!visited.Contains(neighboor))
                    {
                        s.Push(neighboor);
                        visited.Add(neighboor);
                    }
                }
            }

            return -1;
        }
        #endregion
        #region ComponentsInGraph
        /*
         *  Given a Graph formed by a list of edges
         *  Return a list with 2 elements, the size of the small and the size of the greatest segment of the graph
         *  Consider a segment a set of linked nodes. Do not consider unconnected nodes ( single nodes ).
         *  
         */
        // Optimized version
        public static List<int> componentsInGraph(List<List<int>> gb)
        {
            // Get max node number from list of edges
            int nodesCount = Math.Max(gb.Select(p => p[0]).Max(), gb.Select(p => p[1]).Max());

            // Create Adjancency list as array of list of integers
            List<int>[] al = new List<int>[nodesCount];
            for ( int i=0; i<al.Length; i++)
            {
                al[i] = new List<int>();
            }

            // Fill Adjancency list - as this is a undirected graph, for each edge, fill both directions
            foreach ( List<int> edge in gb)
            {
                al[edge[0] - 1].Add(edge[1]);
                al[edge[1] - 1].Add(edge[0]);
            }

            // List with connected nodes
            List<List<int>> connectedNodes = new List<List<int>>();

            // Traverse Graph using Adjancency List
            Queue<int> q = new Queue<int>();
            HashSet<int> visited = new HashSet<int>();
            int c = 0;

            // Start with each node at adjacency List
            for ( int startNode=1; startNode<=al.Length; startNode++)
            {
                if (!visited.Contains(startNode) && al[startNode-1].Count>0)
                {
                    visited.Add(startNode);
                    q.Enqueue(startNode);
                    connectedNodes.Add(new List<int> {});

                    while (q.Count > 0)
                    {
                        int thisNode = q.Dequeue();
                        connectedNodes[c].Add(thisNode);
                        foreach ( int neighbor in al[thisNode - 1])
                        {
                            if (!visited.Contains(neighbor))
                            {
                                visited.Add(neighbor);
                                q.Enqueue(neighbor);
                            }
                        }
                    }
                    c++;
                }
            }

            int minGroup = connectedNodes.Select(p=>p.Count).Min();
            int maxGroup = connectedNodes.Select(p => p.Count).Max();

            return new List<int>() { minGroup, maxGroup };

        }
        // This version works but its not fully optimized
        // Instead of building the Adjacency List its filters edges directly in gb edges list
        public static List<int> componentsInGraph1(List<List<int>> gb)
        {
            // Create a list with all nodes
            List<int> nodes = new List<int>();
            foreach ( List<int> edge in gb)
            {
                if (!nodes.Contains(edge[0]))
                    nodes.Add(edge[0]);
                if (!nodes.Contains(edge[1]))
                    nodes.Add(edge[1]);
            }

            // Visited nodes
            HashSet<int> visited = new HashSet<int>();

            // List with connected nodes
            List<List<int>> connectedNodes = new List<List<int>>();
            int c = 0;

            // For each Node in graph
            foreach ( int node in nodes)
            {
                // If it was not visited yet
                if (!visited.Contains(node))
                {
                    // Add this nodo to connected List
                    connectedNodes.Add(new List<int>());

                    // Traverse the graph starting at this node
                    Queue<int> q = new Queue<int>();
                    q.Enqueue(node);
                    visited.Add(node);

                    while (q.Count > 0)
                    {
                        int currentNode = q.Dequeue();
                        connectedNodes[c].Add(currentNode);

                        foreach ( var edge in gb.Where(p=>p[0]==currentNode))
                        {
                            if (!visited.Contains(edge[1]))
                            {
                                q.Enqueue(edge[1]);
                                visited.Add(edge[1]);
                            }
                        }
                        // twice ... as its not a directed graph, each edge means there is a two way connection
                        foreach (var edge in gb.Where(p => p[1] == currentNode))
                        {
                            if (!visited.Contains(edge[0]))
                            {
                                q.Enqueue(edge[0]);
                                visited.Add(edge[0]);
                            }
                        }
                    }
                    c++;
                }
            }

            var list = connectedNodes.Select(p => p.Count()).Where(p => p > 1).OrderBy(p => p).ToList();
            var uniqueList = new List<int> { list[0], list[list.Count - 1] };
            return uniqueList.ToList();
        }
        #endregion
        #region BeadthFirstSearch_ShortestReach
        /*
         *  Beadth First Search: Shortest Reach:
         *  
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

        // This is the best solution!!!!
        // It uses the Node Class ( see bellow ) to represent the graph
        // Later I found to be easier to build the adjacency list. This version takes more code but does not impact in performace

        public static List<int> Bfs(int n, int m, List<List<int>> edges, int s)
        {
            if (edges.Count == 0)
                return new List<int>();

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
            for (int i = 1; i <= n; i++)
                nodes[i] = -1;
            nodes[s] = 0;

            if (map.ContainsKey(s))
            {
                Queue<Tuple<Node, int>> q = new Queue<Tuple<Node, int>>();
                q.Enqueue(new Tuple<Node, int>(map[s], 0));

                while (q.Count > 0)
                {
                    Tuple<Node, int> nq = q.Dequeue();


                    foreach (Node child in nq.Item1.Edges)
                    {
                        if (nodes[child.Value] == -1)
                        {
                            nodes[child.Value] = nq.Item2 + 1;
                            q.Enqueue(new Tuple<Node, int>(child, nq.Item2 + 1));
                        }
                    }
                }
            }

            // Return list with dephts
            return nodes.Where(p => p != 0).Select(p => p == -1 ? -1 : p * 6).ToList();
        }

        // This version works but does not have the best performace, as it querys directly at edges list each time to find neighbors nodes
        public static List<int> Bfs4(int n, int m, List<List<int>> edges, int s)
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
                        distances[edge[1]] = (node[1] + 1)*6;
                    }
                }
                foreach (List<int> edge in edges.Where(p => p[1] == node[0]))
                {
                    if (distances[edge[0]] == -1 )
                    {
                        queue.Enqueue(new int[] { edge[0], node[1] + 1 });
                        distances[edge[0]] = (node[1] + 1)*6;
                    }
                }
            }

            return distances.Where(p => p != 0).ToList();

        }
        // This is very similar to Bfs4. Just let to multiply edge value ( 6 by definition ) at the end
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
        // This also works fine, but uses the Node class to build the graph representation instead of building the Adjacency List
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
        #endregion
        #region DeepFirstSearch_ShortestReach
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

        // This version calls GetDepth for each node
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
        #endregion
    }
}
