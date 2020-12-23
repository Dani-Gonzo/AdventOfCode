using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{

    class Day01 : ASolution
    {
        int[] ParsedInput;
        int sum = 2020;

        public Day01() : base(01, 2020, "")
        {
            UseDebugInput = false;
            string[] SplitInput = Input.Split("\n");
            ParsedInput = SplitInput.Select(int.Parse).ToArray();
        }

        protected override string SolvePartOne()
        {
            int num1;
            int num2;
            for (int i = 0; i < ParsedInput.Length; i++) {
                num1 = ParsedInput[i];
                for (int j = 1; j < ParsedInput.Length; j++) {
                    num2 = ParsedInput[j];
                    if (num1 + num2 == sum)
                    {
                        int final = num1 * num2;
                        return final.ToString();
                    }
                }
            }
            
            return null;
        }

        protected override string SolvePartTwo()
        {
            int num1, num2, num3;
            for (int i = 0; i < ParsedInput.Length; i++) {
                num1 = ParsedInput[i];
                for (int j = i+1; j < ParsedInput.Length; j++) {
                    num2 = ParsedInput[j];
                    for (int k = j + 1; k < ParsedInput.Length; k++) {
                        num3 = ParsedInput[k];
                        if (num1 + num2 + num3 == sum)
                        {
                            int product = num1 * num2 * num3;
                            return product.ToString();
                        }
                    }
                }
            }
            return null;
        }
    }
}
