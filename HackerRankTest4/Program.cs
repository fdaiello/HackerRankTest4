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
            int n = 9;
            int m = 4;
            int s = 1;

            List<List<int>> edges = new()
            {
                new() { 1, 2 },
                new() { 2, 3 },
                new() { 2, 4 },
                new() { 1, 3 },
                new() { 3,6},
                new() { 3,7},
                new() { 7,8},
                new() { 7,9}
            };

            Console.WriteLine(String.Join(",", Result.bfs(n, m, edges, s)));
            Console.WriteLine("Expected: 1, 2, 2, -1, 3, 3, 4, 4");
        }
    }
}
