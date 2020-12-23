using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day03 : ASolution
    {
        string[,] ParsedInput = new string[0, 0];

        public Day03() : base(03, 2020, "")
        {
            UseDebugInput = false;
            string[] SplitInput = Input.Split("\n");
            int numOfChars = SplitInput[0].Length;
            int numOfRows = SplitInput.Length;
            ParsedInput = new string[numOfChars, numOfRows];
            for (int x = 0; x < numOfChars; x++) {
                for (int y = 0; y < numOfRows; y++) {
                    ParsedInput[x, y] = SplitInput[y][x].ToString();
                }
            }
        }

        protected override string SolvePartOne()
        {
            int treeCount = 0;
            for (int x = 0, y = 0; x < ParsedInput.GetLength(0) && y < ParsedInput.GetLength(1); x += 3, x %= ParsedInput.GetLength(0), y++) {
                if (ParsedInput[x, y] == "#") {
                    treeCount++;
                }
            }
            return treeCount.ToString();
        }

        protected override string SolvePartTwo()
        {
            long treeProduct = 0;

            long treeCount1 = 0;
            for (int x = 0, y = 0; x < ParsedInput.GetLength(0) && y < ParsedInput.GetLength(1); x++, x %= ParsedInput.GetLength(0), y++)
            {
                if (ParsedInput[x, y] == "#")
                {
                    treeCount1++;
                }
            }

            long treeCount2 = 0;
            for (int x = 0, y = 0; x < ParsedInput.GetLength(0) && y < ParsedInput.GetLength(1); x += 3, x %= ParsedInput.GetLength(0), y++)
            {
                if (ParsedInput[x, y] == "#")
                {
                    treeCount2++;
                }
            }

            long treeCount3 = 0;
            for (int x = 0, y = 0; x < ParsedInput.GetLength(0) && y < ParsedInput.GetLength(1); x += 5, x %= ParsedInput.GetLength(0), y++)
            {
                if (ParsedInput[x, y] == "#")
                {
                    treeCount3++;
                }
            }

            long treeCount4 = 0;
            for (int x = 0, y = 0; x < ParsedInput.GetLength(0) && y < ParsedInput.GetLength(1); x += 7, x %= ParsedInput.GetLength(0), y++)
            {
                if (ParsedInput[x, y] == "#")
                {
                    treeCount4++;
                }
            }

            long treeCount5 = 0;
            for (int x = 0, y = 0; x < ParsedInput.GetLength(0) && y < ParsedInput.GetLength(1); x++, x %= ParsedInput.GetLength(0), y += 2)
            {
                if (ParsedInput[x, y] == "#")
                {
                    treeCount5++;
                }
            }

            Console.WriteLine($"Tree1: {treeCount1}, Tree2: {treeCount2}, Tree3: {treeCount3}, Tree4: {treeCount4}, Tree5: {treeCount5}");

            treeProduct = treeCount1 * treeCount2 * treeCount3 * treeCount4 * treeCount5;
            return treeProduct.ToString();
        }
    }
}
