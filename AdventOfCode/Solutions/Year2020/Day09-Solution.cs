using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{

    class Day09 : ASolution
    {
        List<long> numbers = new List<long>();
        int preamble = 25;
        long answerA = 0;

        public Day09() : base(09, 2020, "")
        {
            UseDebugInput = false;
            string[] SplitInput = Input.SplitByNewline();
            foreach (string num in SplitInput) {
                numbers.Add(long.Parse(num));
            }
        }

        protected override string SolvePartOne()
        {
            List<long> numsToEvaluate = new List<long>();
            long wrongNum = 0;

            for (int i = 0; i < preamble; i++) {
                numsToEvaluate.Add(numbers[i]);
            }

            for (int i = preamble + 1; i < numbers.Count; i++) {
                bool isValid = false;
                for (int j = 0; j < numsToEvaluate.Count && !isValid; j++) {
                    long pream1 = numsToEvaluate[j];
                    for (int k = j + 1; k < numsToEvaluate.Count && !isValid; k++) {
                        long pream2 = numsToEvaluate[k];
                        if (pream1 + pream2 == numbers[i]) {
                            isValid = true;
                        }
                    }    
                }
                numsToEvaluate.RemoveAt(0);
                numsToEvaluate.Add(numbers[i]);

                if (!isValid) {
                    wrongNum = numbers[i];
                }
            }
            answerA = wrongNum;
            return wrongNum.ToString();
        }

        protected override string SolvePartTwo()
        {
            List<long> setToEvaluate = new List<long>();
            setToEvaluate.Add(numbers[0]);
            setToEvaluate.Add(numbers[1]);

            int nextNumIndex = 2;
            long sumOfSet = 0;

            while (sumOfSet != answerA) {
                sumOfSet = setToEvaluate.Sum();
                if (sumOfSet < answerA) {
                    setToEvaluate.Add(numbers[nextNumIndex]);
                    nextNumIndex++;
                } else if (sumOfSet > answerA) {
                    setToEvaluate.RemoveAt(0);
                }
            }

            long smallest = setToEvaluate.Min();
            long largest = setToEvaluate.Max();

            long sum = smallest + largest;

            return sum.ToString();
        }
    }
}
