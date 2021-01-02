using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020 {
    class Bag {
        public string color;
        public List<string> bagsOfHolding { get; private set; } = new List<string>();
        public Dictionary<string, int> bagRules { get; private set; } = new Dictionary<string, int>();
    }

    class Day07 : ASolution {
        List<Bag> allTheBags = new List<Bag>();

        public Day07() : base(07, 2020, "") {
            UseDebugInput = false;
            string[] SplitInput = Input.Replace("bags", "").Replace("bag", "").SplitByNewline();
            foreach (string element in SplitInput) {
                Bag newBag = new Bag();
                string[] bagRule = element.Split("contain");
                string[] containedBags = bagRule[1].Split(",").Select(s => s.Trim(' ', '.')).ToArray();
                newBag.color = bagRule[0].Trim();
                foreach (string bag in containedBags) {
                    newBag.bagsOfHolding.Add(bag);
                }
                allTheBags.Add(newBag);
            }
        }

        protected override string SolvePartOne() {
            List<string> bagsToSearch = new List<string>() { "shiny gold" };
            for (int i = 0; i < bagsToSearch.Count; i++) {
                string searchFor = bagsToSearch[i];
                foreach (var bag in allTheBags.Where(b => b.bagsOfHolding.Any(s => s.Contains(searchFor)))) {
                    if (!bagsToSearch.Contains(bag.color)) {
                        bagsToSearch.Add(bag.color);
                    }
                }
            }
            return (bagsToSearch.Count-1).ToString();
        }

        protected override string SolvePartTwo() {
            foreach (var thisBag in allTheBags) {
                foreach (var itemizedBags in thisBag.bagsOfHolding) {
                    string[] parts = itemizedBags.Split(' ', 2);
                    if (parts[0] != "no") { 
                        thisBag.bagRules.Add(parts[1], int.Parse(parts[0]));
                    }
                   
                }
            }

            int numberOfBags(string colorToCount) {
                Bag myBag = allTheBags.Find(b => b.color == colorToCount);
                int containedBags = 0;
                foreach (var pair in myBag.bagRules) {
                    containedBags += pair.Value + (pair.Value * numberOfBags(pair.Key));
                }
                return containedBags;
            }

            return numberOfBags("shiny gold").ToString();
        }
    }
}
