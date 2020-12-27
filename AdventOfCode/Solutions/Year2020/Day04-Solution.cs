using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020
{

    class Day04 : ASolution
    {
        string[] ParsedInput;
        public Day04() : base(04, 2020, "")
        {
            UseDebugInput = false;
            string[] SplitInput = Input.Split(new string[] { "\n\n" }, StringSplitOptions.RemoveEmptyEntries);
            ParsedInput = SplitInput;
        }

        protected override string SolvePartOne()
        {
            int validPassports = 0;
            for (int i = 0; i < ParsedInput.Length; i++) {
                if (ParsedInput[i].Contains("byr") &&
                ParsedInput[i].Contains("iyr") &&
                ParsedInput[i].Contains("eyr") &&
                ParsedInput[i].Contains("hgt") &&
                ParsedInput[i].Contains("hcl") &&
                ParsedInput[i].Contains("ecl") &&
                ParsedInput[i].Contains("pid")) {
                    validPassports++;
                }
            }
            return validPassports.ToString();
        }

        protected override string SolvePartTwo()
        {

            int validPasswords = 0;

            for (int i = 0; i < ParsedInput.Length; i++)
            {
                if (ParsedInput[i].Contains("byr") &&
                ParsedInput[i].Contains("iyr") &&
                ParsedInput[i].Contains("eyr") &&
                ParsedInput[i].Contains("hgt") &&
                ParsedInput[i].Contains("hcl") &&
                ParsedInput[i].Contains("ecl") &&
                ParsedInput[i].Contains("pid"))
                {
                    var SplitInput = ParsedInput[i].Split(new char[] { '\n', ' ' }).Select(element => element.Split(':')).ToDictionary(x => x[0], x => x[1]);
                    int byr = int.Parse(SplitInput["byr"]);
                    int iyr = int.Parse(SplitInput["iyr"]);
                    int eyr = int.Parse(SplitInput["eyr"]);
                    int hgt;

                    if (!(byr >= 1920 && byr <= 2002)) {
                        continue;
                    }

                    if (!(iyr >= 2010 && iyr <= 2020)) {
                        continue;
                    }

                    if (!(eyr >= 2020 && eyr <= 2030)) {
                        continue;
                    }

                    if (SplitInput["hgt"].Contains("in")) {
                        hgt = int.Parse(SplitInput["hgt"].Trim('i', 'n'));
                        if (!(hgt >= 59 && hgt <= 76)) {
                            continue;
                        }
                    } else if (SplitInput["hgt"].Contains("cm")) {
                        hgt = int.Parse(SplitInput["hgt"].Trim('c', 'm'));
                        if (!(hgt >= 150 && hgt <= 193))
                        {
                            continue;
                        }
                    } else {
                        continue;
                    }

                    if (!(Regex.IsMatch(SplitInput["hcl"], "^#[a-f0-9]{6}"))) {
                        continue;
                    }

                    if (!(SplitInput["ecl"] == "amb" |
                    SplitInput["ecl"] == "blu" |
                    SplitInput["ecl"] == "brn" |
                    SplitInput["ecl"] == "gry" |
                    SplitInput["ecl"] == "grn" |
                    SplitInput["ecl"] == "hzl" |
                    SplitInput["ecl"] == "oth")) {
                        continue;
                    }

                    if (!(Regex.IsMatch(SplitInput["pid"], "^\\d{9}$"))) {
                        continue;
                    }

                    validPasswords++;
                }
            }
            return validPasswords.ToString();
        }
    }
}
