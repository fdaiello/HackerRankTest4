using System;
using System.Collections.Generic;

namespace HackerRankTest4
{
    class Program
    {
        static void Main(string[] args)
        {
            TestBFS();
        }

        static void TestBFS()
        {
            int n = 5;
            int m = 4;
            int s = 1;

            List<List<int>> edges = new()
            {
                new() { 1, 2 },
                new() { 2, 3 },
                new() { 2, 4 },
                new() { 1, 3 }
            };

            Console.WriteLine(String.Join(",", Result.bfs(n, m, edges, s)));
        }
    }
}
