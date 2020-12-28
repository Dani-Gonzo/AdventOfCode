using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{

    class Day06 : ASolution
    {
        string[] ParsedInput;
        public Day06() : base(06, 2020, "")
        {
            UseDebugInput = false;
            string[] SplitInput = Input.Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            ParsedInput = SplitInput;
        }

        protected override string SolvePartOne()
        {
            int sumOfCounts = 0;

            foreach (string element in ParsedInput) {
                string cleaned = element.Replace("\n", "");
                string fix = new string (cleaned.ToCharArray().Distinct().ToArray());
                sumOfCounts += fix.Length;
            }
            return sumOfCounts.ToString();
        }

        protected override string SolvePartTwo()
        {
            int sumOfCounts = 0;

            foreach (string element in ParsedInput)
            {
                string[] cleaned = element.Split('\n');
                char[] prevEntry = cleaned[0].ToCharArray();
                char[] currentEntry;
                for (int i = 1; i < cleaned.Length; i++) {
                    currentEntry = cleaned[i].ToCharArray();
                    prevEntry = currentEntry.Intersect(prevEntry).ToArray();
                }
                sumOfCounts += prevEntry.Length;
            }
            return sumOfCounts.ToString();
        }
    }
}
