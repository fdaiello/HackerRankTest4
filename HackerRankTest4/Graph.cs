using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRankTest4
{
    /*
     *  This code was copied from:
     *  https://www.geeksforgeeks.org/delete-edge-minimize-subtree-sum-difference/
     *  
     *  contributed by aashish1995.
     */
    public class Graph
    {

        static int res;

        // DFS method to traverse through edges, 
        // calculating subtree sum at each node 
        // and updating the difference between subtrees
        static void dfs(int u, int parent, int totalSum, List<int>[] edge, int[] subtree)
        {
            int sum = subtree[u];

            // Loop for all neighbors except parent 
            // and aggregate sum over all subtrees
            for (int i = 0; i < edge[u].Count; i++)
            {
                int v = edge[u][i];

                if (v != parent)
                {
                    dfs(v, u, totalSum, edge, subtree);
                    sum += subtree[v];
                }
            }

            // Store sum in current node's subtree index
            subtree[u] = sum;

            // At one side subtree sum is 'sum' and other
            // side subtree sum is 'totalSum - sum' so
            // their difference will be totalSum - 2*sum, 
            // by which we'll update res
            if (u != 0 && Math.Abs(totalSum - 2 * sum) < res)
                res = Math.Abs(totalSum - 2 * sum);
        }

        // Method returns minimum subtree sum difference
        public static int getMinSubtreeSumDifference(int[] vertex, int[,] edges, int N)
        {
            res = int.MaxValue;

            int totalSum = 0;
            int[] subtree = new int[N];

            // Calculating total sum of tree and 
            // initializing subtree sum's by 
            // vertex values
            for (int i = 0; i < N; i++)
            {
                subtree[i] = vertex[i];
                totalSum += vertex[i];
            }

            // Filling edge data structure
            List<int>[] edge = new List<int>[N];
            for (int i = 0; i < N; i++)
            {
                edge[i] = new List<int>();
            }
            for (int i = 0; i < N - 1; i++)
            {
                edge[edges[i, 0]].Add(edges[i, 1]);
                edge[edges[i, 1]].Add(edges[i, 0]);
            }

            // int res = int.MaxValue;

            // Calling DFS method at node 0, with
            // parent as -1
            dfs(0, -1, totalSum, edge, subtree);

            return res;
        }
    }
}