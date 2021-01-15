using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{

    class Day11 : ASolution
    {
        string[,] seats = new string[0, 0];

        public Day11() : base(11, 2020, "")
        {
            UseDebugInput = false;
            string[] SplitInput = Input.SplitByNewline();

            int numOfChars = SplitInput[0].Length;
            int numOfRows = SplitInput.Length;
            seats = new string[numOfChars, numOfRows];
            for (int x = 0; x < numOfChars; x++) {
                for (int y = 0; y < numOfRows; y++) {
                    seats[x, y] = SplitInput[y][x].ToString();
                }
            }
        }

        protected override string SolvePartOne()
        {
            string[,] previousRound = new string[seats.GetLength(0), seats.GetLength(1)]; 
            
            string[,] currentRound = new string[seats.GetLength(0), seats.GetLength(1)];
            Array.Copy(seats, currentRound, seats.Length);

            bool coordCheck(int x, int y, string character) { 
                if (x >= 0 && x < previousRound.GetLength(0) && y >= 0 && y < previousRound.GetLength(1)) {
                    if (previousRound[x, y] == character) { return true; }
                }
                return false;
            }

            int charCheck(int x, int y, string character) {
                int counter = 0;

                if (coordCheck(x - 1, y - 1, character)) { counter++; } // left upper corner
                if (coordCheck(x - 1, y + 1, character)) { counter++; } // left lower corner
                if (coordCheck(x + 1, y - 1, character)) { counter++; } // right upper corner
                if (coordCheck(x + 1, y + 1, character)) { counter++; } // right lower corner
                if (coordCheck(x, y - 1, character)) { counter++; } // top
                if (coordCheck(x, y + 1, character)) { counter++; } // bottom
                if (coordCheck(x - 1, y, character)) { counter++; } // left side
                if (coordCheck(x + 1, y, character)) { counter++; } // right side

                return counter;
            }

            while (!previousRound.Flatten().SequenceEqual(currentRound.Flatten())) {
                Array.Copy(currentRound, previousRound, currentRound.Length);
                for (int x = 0; x < seats.GetLength(0); x++) {
                    for (int y = 0; y < seats.GetLength(1); y++) {
                        if (previousRound[x, y] == "L") {
                            int receivedCount = charCheck(x, y, "#");
                            if (receivedCount == 0) {
                                currentRound[x, y] = "#";
                            }
                        } else if (previousRound[x, y] == "#") {
                            int receivedCount = charCheck(x, y, "#");
                            if (receivedCount >= 4) {
                                currentRound[x, y] = "L";
                            }
                        }
                    }
                }
            }

            int seatCounter = 0;
            for (int x = 0; x < seats.GetLength(0); x++) {
                for (int y = 0; y < seats.GetLength(1); y++) { 
                    if (currentRound[x, y] == "#") {
                        seatCounter++;
                    }  
                }
                
            }

            return seatCounter.ToString();
        }

        protected override string SolvePartTwo()
        {
            string[,] previousRound = new string[seats.GetLength(0), seats.GetLength(1)];

            string[,] currentRound = new string[seats.GetLength(0), seats.GetLength(1)];
            Array.Copy(seats, currentRound, seats.Length);

            bool coordCheck(int x, int y, int directionX, int directionY, string character) {
                x += directionX;
                y += directionY;
                while (x >= 0 && x < previousRound.GetLength(0) && y >= 0 && y < previousRound.GetLength(1)) {
                    if (previousRound[x, y] == "L" | previousRound[x, y] == "#") {
                        if (previousRound[x, y] != character) { return false; }
                        if (previousRound[x, y] == character) { return true; }
                    } else if (previousRound[x, y] == ".") {
                        x += directionX;
                        y += directionY;
                    }
                }
                return false;
            }

            int charCheck(int x, int y, string character) {
                int counter = 0;

                if (coordCheck(x, y, -1, -1, character)) { counter++; }
                if (coordCheck(x, y, -1, 1, character)) { counter++; }
                if (coordCheck(x, y, 1, -1, character)) { counter++; }
                if (coordCheck(x, y, 1, 1, character)) { counter++; }
                if (coordCheck(x, y, 0, -1, character)) { counter++; }
                if (coordCheck(x, y, 0, 1, character)) { counter++; }
                if (coordCheck(x, y, -1, 0, character)) { counter++; }
                if (coordCheck(x, y, 1, 0, character)) { counter++; }

                return counter;
            }

            while (!previousRound.Flatten().SequenceEqual(currentRound.Flatten())) {
                Array.Copy(currentRound, previousRound, currentRound.Length);
                for (int x = 0; x < seats.GetLength(0); x++) {
                    for (int y = 0; y < seats.GetLength(1); y++) {
                        if (previousRound[x, y] == "L") {
                            int receivedCount = charCheck(x, y, "#");
                            if (receivedCount == 0) {
                                currentRound[x, y] = "#";
                            }
                        } else if (previousRound[x, y] == "#") {
                            int receivedCount = charCheck(x, y, "#");
                            if (receivedCount >= 5) {
                                currentRound[x, y] = "L";
                            }
                        }
                    }
                }
            }

            int seatCounter = 0;
            for (int x = 0; x < seats.GetLength(0); x++) {
                for (int y = 0; y < seats.GetLength(1); y++) {
                    if (currentRound[x, y] == "#") {
                        seatCounter++;
                    }
                }

            }

            return seatCounter.ToString();
        }
    }
}
