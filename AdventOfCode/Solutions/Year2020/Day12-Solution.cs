using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{
    class Navigation {
        public char command;
        public int value;
    }

    class Day12 : ASolution
    {
        List<Navigation> navigation = new List<Navigation>();

        public Day12() : base(12, 2020, "")
        {
            UseDebugInput = false;
            string[] SplitInput = Input.SplitByNewline();
            foreach (string element in SplitInput) {
                Navigation newNav = new Navigation();
                newNav.command = Convert.ToChar(element.Substring(0, 1));
                newNav.value = int.Parse(element.Substring(1));
                navigation.Add(newNav);
            }
        }

        protected override string SolvePartOne()
        {
            string currentFacing = "east";
            int east = 0;
            int west = 0;
            int north = 0;
            int south = 0;

            foreach (var instruction in navigation) {
                if (instruction.command == 'N') {
                    north += instruction.value;
                } else if (instruction.command == 'S') {
                    south += instruction.value;
                } else if (instruction.command == 'E') {
                    east += instruction.value;
                } else if (instruction.command == 'W') {
                    west += instruction.value;
                } else if (instruction.command == 'L') {
                    if (currentFacing == "east" && instruction.value == 90) { currentFacing = "north"; }
                    else if (currentFacing == "east" && instruction.value == 180) { currentFacing = "west"; }
                    else if (currentFacing == "east" && instruction.value == 270) { currentFacing = "south"; }
                    else if (currentFacing == "west" && instruction.value == 90) { currentFacing = "south"; }
                    else if (currentFacing == "west" && instruction.value == 180) { currentFacing = "east"; }
                    else if (currentFacing == "west" && instruction.value == 270) { currentFacing = "north"; }
                    else if (currentFacing == "north" && instruction.value == 90) { currentFacing = "west"; }
                    else if (currentFacing == "north" && instruction.value == 180) { currentFacing = "south"; }
                    else if (currentFacing == "north" && instruction.value == 270) { currentFacing = "east"; }
                    else if (currentFacing == "south" && instruction.value == 90) { currentFacing = "east"; }
                    else if (currentFacing == "south" && instruction.value == 180) { currentFacing = "north"; }
                    else if (currentFacing == "south" && instruction.value == 270) { currentFacing = "west"; }
                } else if (instruction.command == 'R') {
                    if (currentFacing == "east" && instruction.value == 90) { currentFacing = "south"; } 
                    else if (currentFacing == "east" && instruction.value == 180) { currentFacing = "west"; } 
                    else if (currentFacing == "east" && instruction.value == 270) { currentFacing = "north"; } 
                    else if (currentFacing == "west" && instruction.value == 90) { currentFacing = "north"; } 
                    else if (currentFacing == "west" && instruction.value == 180) { currentFacing = "east"; }
                    else if (currentFacing == "west" && instruction.value == 270) { currentFacing = "south"; }
                    else if (currentFacing == "north" && instruction.value == 90) { currentFacing = "east"; } 
                    else if (currentFacing == "north" && instruction.value == 180) { currentFacing = "south"; }
                    else if (currentFacing == "north" && instruction.value == 270) { currentFacing = "west"; }
                    else if (currentFacing == "south" && instruction.value == 90) { currentFacing = "west"; } 
                    else if (currentFacing == "south" && instruction.value == 180) { currentFacing = "north"; } 
                    else if (currentFacing == "south" && instruction.value == 270) { currentFacing = "east"; }
                } else if (instruction.command == 'F') {
                    if (currentFacing == "east") { east += instruction.value; }
                    if (currentFacing == "west") { west += instruction.value; }
                    if (currentFacing == "north") { north += instruction.value; }
                    if (currentFacing == "south") { south += instruction.value; }
                }
            }

            int eastWest = Math.Abs(east - west);
            int northSouth = Math.Abs(north - south);

            return (northSouth + eastWest).ToString();
        }

        protected override string SolvePartTwo()
        {
            int east = 0;
            int west = 0;
            int north = 0;
            int south = 0;

            int waypointEastWest = 10;
            int waypointNorthSouth = 1;

            foreach (var instruction in navigation) {
                if (instruction.command == 'N') {
                    waypointNorthSouth += instruction.value;
                } else if (instruction.command == 'S') {
                    waypointNorthSouth -= instruction.value;
                } else if (instruction.command == 'E') {
                    waypointEastWest += instruction.value;
                } else if (instruction.command == 'W') {
                    waypointEastWest -= instruction.value;

                } else if (instruction.command == 'L') {
                    int waypointEast = waypointEastWest > 0 ? waypointEastWest : 0;
                    int waypointWest = waypointEastWest < 0 ? -waypointEastWest : 0;
                    int waypointNorth = waypointNorthSouth > 0 ? waypointNorthSouth : 0;
                    int waypointSouth = waypointNorthSouth < 0 ? -waypointNorthSouth : 0;

                    int tempWayEast = 0;
                    int tempWayWest = 0;
                    int tempWayNorth = 0;
                    int tempWaySouth = 0;

                    if (instruction.value == 90 && waypointEast != 0) { tempWayNorth = waypointEast; }
                    if (instruction.value == 180 && waypointEast != 0) { tempWayWest = waypointEast; }
                    if (instruction.value == 270 && waypointEast != 0) { tempWaySouth = waypointEast; }
                    if (instruction.value == 90 && waypointWest != 0) { tempWaySouth = waypointWest; }
                    if (instruction.value == 180 && waypointWest != 0) { tempWayEast = waypointWest; }
                    if (instruction.value == 270 && waypointWest != 0) { tempWayNorth = waypointWest; }
                    if (instruction.value == 90 && waypointNorth != 0) { tempWayWest = waypointNorth; }
                    if (instruction.value == 180 && waypointNorth != 0) { tempWaySouth = waypointNorth; }
                    if (instruction.value == 270 && waypointNorth != 0) { tempWayEast = waypointNorth; }
                    if (instruction.value == 90 && waypointSouth != 0) { tempWayEast = waypointSouth; }
                    if (instruction.value == 180 && waypointSouth != 0) { tempWayNorth = waypointSouth; }
                    if (instruction.value == 270 && waypointSouth != 0) { tempWayWest = waypointSouth; }

                    waypointEastWest = tempWayEast != 0 ? tempWayEast : -tempWayWest;
                    waypointNorthSouth = tempWayNorth != 0 ? tempWayNorth : -tempWaySouth;

                } else if (instruction.command == 'R') {
                    // rotate waypoint clockwise around ship

                    int waypointEast = waypointEastWest > 0 ? waypointEastWest : 0;
                    int waypointWest = waypointEastWest < 0 ? -waypointEastWest : 0;
                    int waypointNorth = waypointNorthSouth > 0 ? waypointNorthSouth : 0;
                    int waypointSouth = waypointNorthSouth < 0 ? -waypointNorthSouth : 0;

                    int tempWayEast = 0;
                    int tempWayWest = 0;
                    int tempWayNorth = 0;
                    int tempWaySouth = 0;

                    if (instruction.value == 90 && waypointEast != 0) { tempWaySouth = waypointEast; } 
                    if (instruction.value == 180 && waypointEast != 0) { tempWayWest = waypointEast; } 
                    if (instruction.value == 270 && waypointEast != 0) { tempWayNorth = waypointEast; } 
                    if (instruction.value == 90 && waypointWest != 0) { tempWayNorth = waypointWest; }
                    if (instruction.value == 180 && waypointWest != 0) { tempWayEast = waypointWest; }
                    if (instruction.value == 270 && waypointWest != 0) { tempWaySouth = waypointWest; }
                    if (instruction.value == 90 && waypointNorth != 0) { tempWayEast = waypointNorth; }
                    if (instruction.value == 180 && waypointNorth != 0) { tempWaySouth = waypointNorth; }
                    if (instruction.value == 270 && waypointNorth != 0) { tempWayWest = waypointNorth; }
                    if (instruction.value == 90 && waypointSouth != 0) { tempWayWest = waypointSouth; }
                    if (instruction.value == 180 && waypointSouth != 0) { tempWayNorth = waypointSouth; }
                    if (instruction.value == 270 && waypointSouth != 0) { tempWayEast = waypointSouth; }

                    waypointEastWest = tempWayEast != 0 ? tempWayEast : -tempWayWest;
                    waypointNorthSouth = tempWayNorth != 0 ? tempWayNorth : -tempWaySouth;
                } else if (instruction.command == 'F') {
                    east += waypointEastWest > 0 ? waypointEastWest * instruction.value : 0;
                    west += waypointEastWest < 0 ? -waypointEastWest * instruction.value : 0;
                    north += waypointNorthSouth > 0 ? waypointNorthSouth * instruction.value : 0;
                    south += waypointNorthSouth < 0 ? -waypointNorthSouth * instruction.value : 0;
                }
            }

            int eastWest = Math.Abs(east - west);
            int northSouth = Math.Abs(north - south);

            return (northSouth + eastWest).ToString();
        }
    }
}
