using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day05 : ASolution
    {
        string[] SplitInput;
        public Day05() : base(05, 2020, "")
        {
            UseDebugInput = false;
            SplitInput = Input.Split("\n");
        }

        protected override string SolvePartOne()
        {
            int minRow = 0;
            int maxRow = 127;
            int minSeat = 0;
            int maxSeat = 7;

            int prevId = 0;

            for (int i = 0; i < SplitInput.Length; i++) {
                minRow = 0;
                maxRow = 127;
                minSeat = 0;
                maxSeat = 7;

                foreach (char c in SplitInput[i]) {
                    if (c == 'F') {
                        maxRow = ((maxRow - minRow) / 2 + minRow);
                    } else if (c == 'B') {
                        minRow = ((maxRow - minRow) / 2 + minRow + 1);
                    } else if (c == 'R') {
                        minSeat = ((maxSeat - minSeat) / 2 + minSeat + 1);
                    } else if (c == 'L') {
                        maxSeat = ((maxSeat - minSeat) / 2 + minSeat);
                    }
                }

                int nextId = (minRow * 8) + minSeat;
                if (nextId > prevId) {
                    prevId = nextId;
                }
            }

            return prevId.ToString();
        }

        protected override string SolvePartTwo()
        {
            List<int> id = new List<int>();
            int minRow = 0;
            int maxRow = 127;
            int minSeat = 0;
            int maxSeat = 7;

            for (int i = 0; i < SplitInput.Length; i++)
            {
                minRow = 0;
                maxRow = 127;
                minSeat = 0;
                maxSeat = 7;

                foreach (char c in SplitInput[i])
                {
                    if (c == 'F')
                    {
                        maxRow = ((maxRow - minRow) / 2 + minRow);
                    }
                    else if (c == 'B')
                    {
                        minRow = ((maxRow - minRow) / 2 + minRow + 1);
                    }
                    else if (c == 'R')
                    {
                        minSeat = ((maxSeat - minSeat) / 2 + minSeat + 1);
                    }
                    else if (c == 'L')
                    {
                        maxSeat = ((maxSeat - minSeat) / 2 + minSeat);
                    }
                }
                id.Add((minRow * 8) + minSeat);
            }
            id.Sort();
            int mySeat = 0;
            for (int i = 0; i < id.Count-1; i++) {
                if (id[i+1] - id[i] > 1) {
                    int highSeat = id[i + 1];
                    mySeat = highSeat - 1;
                }
            }

            return mySeat.ToString();
        }
    }
}
