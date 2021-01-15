using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{

    class Day10 : ASolution
    {
        List<int> joltages = new List<int>();
        int startingJolt = 0;

        public Day10() : base(10, 2020, "")
        {
            UseDebugInput = false;
            string[] SplitInput = Input.SplitByNewline();
            foreach (string jolt in SplitInput) {
                joltages.Add(int.Parse(jolt));
            }
        }

        protected override string SolvePartOne()
        {
            int oneJoltDiff = 0;
            int threeJoltDiff = 1;

            joltages.Sort();
            if (joltages[0] - startingJolt == 1) {
                oneJoltDiff++;
            } else if (joltages[0] - startingJolt == 3) {
                threeJoltDiff++;
            }

            for (int i = 1; i < joltages.Count; i++) {
                if (joltages[i] - joltages[i-1] == 1) {
                    oneJoltDiff++;
                } else if (joltages[i] - joltages[i-1] == 3) {
                    threeJoltDiff++;
                }
            }

            return (oneJoltDiff * threeJoltDiff).ToString();
        }

        protected override string SolvePartTwo()
        {
            joltages.Sort();
            int lastNum = joltages.Last();

            Dictionary<int, long> cache = new Dictionary<int, long>();

            long adapterHopsCount(int adapter) {
                if (cache.TryGetValue(adapter, out var pathCombo)) {
                    return pathCombo;
                }
                long pathCount = 0;
                if (adapter == lastNum) {
                    return 1;
                }
                if (joltages.Contains(adapter + 1)) {
                    pathCount += adapterHopsCount(adapter + 1);
                } 
                if (joltages.Contains(adapter + 2)) {
                    pathCount += adapterHopsCount(adapter + 2);
                }
                if (joltages.Contains(adapter + 3)) {
                    pathCount += adapterHopsCount(adapter + 3);
                }
                cache.Add(adapter, pathCount);
                return pathCount;
            }

            return adapterHopsCount(0).ToString();
        }
    }
}
