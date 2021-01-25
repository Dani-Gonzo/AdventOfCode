using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    class TicketRules {
        public string name;
        public List<(int min, int max)> ranges = new List<(int min, int max)>();
    }

    class Day16 : ASolution
    {
        List<TicketRules> rules = new List<TicketRules>();
        List<int> ticket = new List<int>();
        List<int[]> nearTickets = new List<int[]>();

        public Day16() : base(16, 2020, "")
        {
            UseDebugInput = false;
            string[] SplitInput = Input.Split("\n\n");
            string[] rulesArray = SplitInput[0].SplitByNewline();
            string[] myTicket = SplitInput[1].SplitByNewline();
            string[] nearbyTickets = SplitInput[2].SplitByNewline();

            foreach (string rule in rulesArray) {
                TicketRules newRule = new TicketRules();
                string[] splitRule = rule.Split(new string[] { ": ", " or " }, StringSplitOptions.None);
                newRule.name = splitRule[0];
                for (int i = 1; i <= 2; i++) {
                    int[] splitRange = splitRule[i].Split('-').Select(int.Parse).ToArray();
                    newRule.ranges.Add((splitRange[0], splitRange[1]));
                }
                rules.Add(newRule);
            }

            for (int i = 1; i <= 1; i++) {
                int[] numbers = myTicket[i].Split(',').Select(int.Parse).ToArray();
                foreach (int number in numbers) {
                    ticket.Add(number);
                }
            }

            for (int i = 1; i < nearbyTickets.Length; i++) {
                int[] numbers = nearbyTickets[i].Split(',').Select(int.Parse).ToArray();
                nearTickets.Add(numbers);
            }
        }

        protected override string SolvePartOne()
        {
            var rangeList = rules.SelectMany(rn => rn.ranges).ToList();
            List<int> invalidValues = new List<int>();

            foreach (int[] ticketList in nearTickets) {
                foreach (int ticketNum in ticketList) {
                    bool matchFound = false;
                    foreach (var range in rangeList) {
                        if (ticketNum >= range.min && ticketNum <= range.max) {
                            matchFound = true;
                        }
                    }
                    if (!matchFound) {
                        invalidValues.Add(ticketNum);
                    }
                }
            }

            return invalidValues.Sum().ToString();
        }

        protected override string SolvePartTwo()
        {
            var rangeList = rules.SelectMany(rn => rn.ranges).ToList();
            List<int[]> validTickets = new List<int[]>();

            bool isValid = false;


            foreach (int[] ticketList in nearTickets) {
                int matchFound = 0;
                foreach (int ticketNum in ticketList) {
                    foreach (var range in rangeList) {
                        if (ticketNum >= range.min && ticketNum <= range.max) {
                            matchFound++;
                            break;
                        }
                    }
                }
                if (matchFound == ticketList.Length) {
                    validTickets.Add(ticketList);
                    isValid = false;
                }
            }

            int numOfColumns = 20; // 20 real input
            List<int[]> columns = new List<int[]>();

            for (int i = 0; i < numOfColumns; i++) {
                int[] set = new int[validTickets.Count];
                for (int j = 0; j < validTickets.Count; j++) {
                    set[j] = validTickets[j][i];
                }
                columns.Add(set);
            }

            // TODO: Check "columns" list against "rules" list TicketRules
            Dictionary<string, int> myTicketAssigned = new Dictionary<string, int>();

            // for each column, check every rule
            // TODO: Check why it's not assigning row or seat from debug input
            while (myTicketAssigned.Count < numOfColumns) {
                for (int i = 0; i < columns.Count; i++) {
                    if (myTicketAssigned.ContainsValue(i)) { continue; }
                    List<string> temp = new List<string>();
                    foreach (TicketRules rule in rules) {
                        if (!myTicketAssigned.ContainsKey(rule.name)) {
                            int matchesFound = 0;
                            foreach (int num in columns[i]) {
                                foreach (var range in rule.ranges) {
                                    if (num >= range.min && num <= range.max) {
                                        matchesFound++;
                                    }
                                }
                                if (matchesFound == columns[i].Length) {
                                    temp.Add(rule.name);
                                }
                            }
                        }
                    }
                    if (temp.Count == 1) {
                        myTicketAssigned.Add(temp[0], i);
                    }
                }
            }

            List<long> myTicketDepartureNumbers = new List<long>();

            foreach (var position in myTicketAssigned) {
                if (position.Key.StartsWith("departure")) {
                    myTicketDepartureNumbers.Add(ticket[position.Value]);
                }
            }


            return myTicketDepartureNumbers.Aggregate((leftSide, rightSide) => leftSide * rightSide).ToString();
        }
    }
}
