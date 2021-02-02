using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{

    class Day18 : ASolution
    {
        List<string> expressions = new List<string>();

        public Day18() : base(18, 2020, "")
        {
            UseDebugInput = false;
            string[] SplitInput = Input.SplitByNewline();
            foreach (string element in SplitInput) {
                expressions.Add(element.Replace(" ", ""));
            }
        }

        long OperatorParsing(string expression, int startPoint, out int resumePoint) {
            long answer = 0;
            long? leftSide = null;
            for (int i = startPoint; i < expression.Length; i += 2) {
                if (expression[i] == ')') {
                    resumePoint = i + 1;
                    return leftSide.Value;
                }
                if (leftSide == null) {
                    if (expression[i] == '(') {
                        leftSide = OperatorParsing(expression, i + 1, out i);
                    } else {
                        leftSide = int.Parse(expression[i].ToString());
                        i++;
                    }
                }
                string op = expression[i].ToString();
                long rightSide = 0;
                if (expression[i + 1] == '(') {
                    rightSide = OperatorParsing(expression, i + 2, out i);
                    i -= 2;
                } else {
                    rightSide = int.Parse(expression[i + 1].ToString());    
                }

                if (op == "+") {
                    answer = leftSide.Value + rightSide;
                } else if (op == "*") {
                    answer = leftSide.Value * rightSide;
                }
                leftSide = answer;
            }
            resumePoint = expression.Length;
            return answer;
        }

        protected override string SolvePartOne()
        {
            long finalAnswer = 0;
            foreach (string expression in expressions) {
                finalAnswer += OperatorParsing(expression, 0, out int trash);
            }

            return finalAnswer.ToString();
        }

        long OperatorParsing2(string expression, int startPoint, out int resumePoint) {
            long answer = 0;
            long? leftSide = null;
            for (int i = startPoint; i < expression.Length; i += 2) {
                if (expression[i] == ')') {
                    resumePoint = i;
                    return leftSide.Value;
                }
                if (leftSide == null) {
                    if (expression[i] == '(') {
                        leftSide = OperatorParsing2(expression, i + 1, out i);
                    } else {
                        leftSide = int.Parse(expression[i].ToString());
                    }
                    i++;

                    if (i >= expression.Length) {
                        resumePoint = expression.Length;
                        return leftSide.Value;
                    }
                }


                string op = expression[i].ToString();
                long rightSide = 0;
                if (expression[i] == ')') {
                    resumePoint = i;
                    return leftSide.Value;
                }
                if (op == "*") {
                    rightSide = OperatorParsing2(expression, i + 1, out i);
                    i -= 2; // might not work as expected
                } else if (expression[i + 1] == '(') {
                    rightSide = OperatorParsing2(expression, i + 2, out i);
                    i -= 1;
                } else {
                    rightSide = int.Parse(expression[i + 1].ToString());
                }

                if (op == "+") {
                    answer = leftSide.Value + rightSide;
                } else if (op == "*") {

                    answer = leftSide.Value * rightSide;
                }
                leftSide = answer;
            }
            resumePoint = expression.Length;
            return answer;
        }

        protected override string SolvePartTwo()
        {
            long finalAnswer = 0;
            foreach (string expression in expressions) {
                finalAnswer += OperatorParsing2(expression, 0, out int trash);
            }

            return finalAnswer.ToString();
        }
    }
}
