using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    class Day17 : ASolution
    {
        Dictionary<(int x, int y, int z, int w), char> coordinates = new Dictionary<(int x, int y, int z, int w), char>();
        public Day17() : base(17, 2020, "")
        {
            UseDebugInput = false;
            string[] SplitInput = Input.SplitByNewline();
            for (int i = 0; i < SplitInput.Length; i++) {
                for (int j = 0; j < SplitInput[i].Length; j++) {
                    if (SplitInput[i][j] == '#') { 
                        coordinates.Add((i, j, 0, 0), SplitInput[i][j]);                        
                    }

                }
            }
        }

        // function for core simulation
        Dictionary<(int x, int y, int z, int w), char> Simulation(Dictionary<(int x, int y, int z, int w), char> currentPocket, bool fourDimensional) {
            var nextPocket = new Dictionary<(int x, int y, int z, int w), char>();
            var keys = currentPocket.Keys;

            foreach (int dx in Enumerable.Range(keys.Min(threeValues => threeValues.x) - 1, (keys.Max(threeValues => threeValues.x)+ 1 - (keys.Min(threeValues => threeValues.x)-1)) + 1)) {
                foreach (int dy in Enumerable.Range(keys.Min(threeValues => threeValues.y) - 1, (keys.Max(threeValues => threeValues.y)+1 - (keys.Min(threeValues => threeValues.y)-1)) + 1)) {
                    foreach (int dz in Enumerable.Range(keys.Min(threeValues => threeValues.z) - 1, (keys.Max(threeValues => threeValues.z) + 1 - (keys.Min(threeValues => threeValues.z)-1)) + 1)) {
                        IEnumerable<int> threeOrFour = Enumerable.Range(0, 1);
                        if (fourDimensional) { threeOrFour = Enumerable.Range(keys.Min(threeValues => threeValues.w) - 1, (keys.Max(threeValues => threeValues.w) + 1 - (keys.Min(threeValues => threeValues.w) - 1)) + 1); }
                        foreach (int dw in threeOrFour) {
                            if (currentPocket.ContainsKey((dx, dy, dz, dw))) {
                                // cube at coord is active, call neighbor count
                                int activeNeighborCount = NeighborCellCount(dx, dy, dz, dw, currentPocket, fourDimensional);
                                if (activeNeighborCount == 2 || activeNeighborCount == 3) {
                                    nextPocket.Add((dx, dy, dz, dw), '#');
                                }
                            } else {
                                // cube is not active, call neighbor count
                                int activeNeighborCount = NeighborCellCount(dx, dy, dz, dw, currentPocket, fourDimensional);
                                if (activeNeighborCount == 3) {
                                    nextPocket.Add((dx, dy, dz, dw), '#');
                                }
                            }
                        }
                    }
                }
            }

            return nextPocket;
        }

        // function for telling how many neighbor cells are active given a coord
        int NeighborCellCount(int x, int y, int z, int w, Dictionary<(int x, int y, int z, int w), char> cubesToEvaluate, bool fourDimensional) {
            int activeCells = 0;
            int wMin = 0;
            int wMax = 0;
            if (fourDimensional) {
                wMin = w - 1;
                wMax = w + 1;
            }
            // check neighbor cells of given cell for active or inactive
            // three for loops
            for (int xCoord = x - 1; xCoord <= x + 1; xCoord++) {
                for (int yCoord = y - 1; yCoord <= y + 1; yCoord++) {
                    for (int zCoord = z - 1; zCoord <= z + 1; zCoord++) {
                        for (int wCoord = wMin; wCoord <= wMax; wCoord++) {
                            if (!(xCoord == x && yCoord == y && zCoord == z && wCoord == w)) {
                                if (cubesToEvaluate.TryGetValue((xCoord, yCoord, zCoord, wCoord), out char cubeChar) && cubeChar == '#') {
                                    activeCells++;
                                }
                            }
                        }

                    }
                }
            }

            return activeCells;
        }

        protected override string SolvePartOne()
        {
            var pd = coordinates;
            int numOfCycles = 0;
            while (numOfCycles < 6) {
                pd = Simulation(pd, false);
                numOfCycles++;
            }

            return pd.Count.ToString();
        }

        protected override string SolvePartTwo()
        {
            var pd = coordinates;
            int numOfCycles = 0;
            while (numOfCycles < 6) {
                pd = Simulation(pd, true);
                numOfCycles++;
            }

            return pd.Count.ToString();
            return null;
        }
    }
}
