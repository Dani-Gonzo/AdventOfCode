using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{

    class Day15 : ASolution
    {
        /// <summary>
        /// key: turn number, value: number spoken
        /// </summary>
        Dictionary<int, int> numbersSpoken = new Dictionary<int, int>();

        public Day15() : base(15, 2020, "")
        {
            UseDebugInput = false;
            int[] splitInput = Input.Split(",").Select(int.Parse).ToArray();
            for (int i = 1; i-1 < splitInput.Length; i++) {
                numbersSpoken.Add(i, splitInput[i-1]);
            }
        }

        long determineNumber(int whichNum) { 
            for (int i = numbersSpoken.Count + 1; i <= whichNum; i++) {
                int nextNum = 0;
                List<int> prevKeys = new List<int>();

                long num = numbersSpoken[i - 1];
                for (int j = i - 1; j > 0; j--) {
                    long jValue = numbersSpoken[j];
                    if (jValue == num) {
                        prevKeys.Add(j);
                        if (prevKeys.Count >= 2) { 
                            nextNum = prevKeys[0] - prevKeys[1];
                            break;
                        }
                    }
                }
                numbersSpoken.Add(i, nextNum);
            }

            return numbersSpoken[whichNum];
        }
        
        protected override string SolvePartOne()
        {
            return determineNumber(2020).ToString();
        }

        protected override string SolvePartTwo()
        {
            Dictionary<int, int> numCache = new Dictionary<int, int>();
            int[] SplitInput = Input.Split(',').Select(int.Parse).ToArray();
            int index = 0;
            int lastNum = -1;

            foreach (int num in SplitInput) {
                if (lastNum >= 0) {
                    numCache[lastNum] = index - 1;
                }
                index++;
                lastNum = num;
            }

            for (int i = index; i < 30000000; i++) {
                int diff = 0;
                if (numCache.TryGetValue(lastNum, out int prevIndex)) {
                    diff = (index - 1) - prevIndex;
                }
                numCache[lastNum] = index - 1;
                lastNum = diff;
                index++;
            }

            return lastNum.ToString();
        }
    }
}
