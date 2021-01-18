using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace AdventOfCode.Solutions.Year2020
{
    class IdCompare {
        public long busId;
        public long openOffset;
    }

    class Day13 : ASolution
    {
        int earliestTimestamp = 0;
        List<int> busIDs = new List<int>();
        List<string> busIdSchedule = new List<string>();

        public Day13() : base(13, 2020, "")
        {
            UseDebugInput = false;
            string[] SplitInput = Input.SplitByNewline();
            earliestTimestamp = int.Parse(SplitInput[0]);
            string[] busIdList = SplitInput[1].Split(",");
            foreach (string element in busIdList) {
                busIdSchedule.Add(element);
                if (element != "x") { 
                    busIDs.Add(int.Parse(element));
                }
            }
        }

        protected override string SolvePartOne()
        {
            int busIdToTake = 0;
            int minToWait = 0; // bus timestamp - earliest timestamp
            Dictionary<int, int> busIDsAndTimestamps = new Dictionary<int, int>(); // bus ids and their timestamps that are one level higher than the earliest timestamp

            foreach (int element in busIDs) {
                int timestamp = (earliestTimestamp / element + 1) * element;
                busIDsAndTimestamps.Add(element, timestamp);
            }

            var firstBus = (busIDsAndTimestamps.OrderBy(bus => bus.Value)).First();
            busIdToTake = firstBus.Key;
            minToWait = firstBus.Value - earliestTimestamp;

            return (busIdToTake * minToWait).ToString();
        }

        protected override string SolvePartTwo()
        {
            Dictionary<int, int> busIndexesAndIds = new Dictionary<int, int>();

            for (int i = 0; i < busIdSchedule.Count; i++) {
                if (busIdSchedule[i] != "x") {
                    busIndexesAndIds.Add(i, int.Parse(busIdSchedule[i]));
                }
            }

            List<IdCompare> busses = new List<IdCompare>();

            foreach (var element in busIndexesAndIds) {
                busses.Add(new IdCompare { busId = element.Value, openOffset = (element.Value - (element.Key % element.Value))});
            }

            long jumpBy = busses[0].busId;
            bool found = false;
            int solveIndex = 1;
            long T = 0;
            long lastIntersection = 0;

            while (!found) {
                if (T % busses[solveIndex].busId == busses[solveIndex].openOffset) {
                    if (solveIndex + 1 >= busses.Count) { break; }
                    //Trace.WriteLine($"{busses[solveIndex].busId}: {T}");
                    if (lastIntersection == 0) {
                        lastIntersection = T;
                    } else {
                        jumpBy = T - lastIntersection;
                        lastIntersection = 0;
                        solveIndex++;
                    }
                    
                }
                T += jumpBy;
            }

            return T.ToString();
        }
    }
}
