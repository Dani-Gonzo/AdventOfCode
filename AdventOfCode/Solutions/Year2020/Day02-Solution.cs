using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{
    class Rule {
        public int minNum;
        public int maxNum;
        public char rule;
        public string password;
    }

    class Day02 : ASolution
    {
        List<Rule> ParsedInput = new List<Rule>();

        public Day02() : base(02, 2020, "")
        {
            UseDebugInput = false;
            string[] SplitInput = Input.Split("\n");
            foreach (string element in SplitInput) {
                string[] pieces = element.Split(new char[] { '-', ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);
                var ruleEntry = new Rule { minNum = Int32.Parse(pieces[0]), maxNum = Int32.Parse(pieces[1]), rule = char.Parse(pieces[2]), password = pieces[3] };
                ParsedInput.Add(ruleEntry);
            }
        }

        protected override string SolvePartOne()
        {
            int validPasswords = 0;
            ParsedInput.ForEach(item => {
                int validChars = 0;
                foreach (char c in item.password) {
                    if (c == item.rule) {
                        validChars++;
                    }
                }
                if (validChars >= item.minNum && validChars <= item.maxNum) {
                    validPasswords++;
                }
            });
            return validPasswords.ToString();
        }

        protected override string SolvePartTwo()
        {
            int validPasswords = 0;
            ParsedInput.ForEach(item => {
                //int validChars = 0;
                if (item.password[item.minNum-1] == item.rule ^ item.password[item.maxNum-1] == item.rule) {
                    validPasswords++;
                }
            });

            return validPasswords.ToString();
        }
    }
}
