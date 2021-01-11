using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode.Solutions.Year2020
{
    class Instruction {
        public string command;
        public string number;
    }

    class Day08 : ASolution
    {
        List<Instruction> instructions = new List<Instruction>();

        public Day08() : base(08, 2020, "")
        {
            UseDebugInput = false;
            string[] SplitInput = Input.SplitByNewline();
            foreach (string element in SplitInput) {
                Instruction newInst = new Instruction();
                string[] command = element.Split(" ");
                newInst.command = command[0];
                newInst.number = command[1];
                instructions.Add(newInst);
            }
        }

        protected override string SolvePartOne()
        {
            int total = 0;
            int index = 0;
            List<int> pastIndexes = new List<int>();
            Instruction commandToExecute = instructions[index];

            while (!pastIndexes.Contains(index)) {
                pastIndexes.Add(index);
                if (commandToExecute.command == "acc") {
                    total += int.Parse(commandToExecute.number);
                    index += 1;
                } else if (commandToExecute.command == "jmp") {
                    index += int.Parse(commandToExecute.number);
                } else if (commandToExecute.command == "nop") {
                    index += 1;
                }
                commandToExecute = instructions[index];
            }

            return total.ToString();
        }

        protected override string SolvePartTwo()
        {
            int CheckLoop() {
                int total = 0;
                int index = 0;
                List<int> pastIndexes = new List<int>();
                Instruction commandToExecute = instructions[index];

                while (!pastIndexes.Contains(index) && index < instructions.Count) {
                    pastIndexes.Add(index);
                    commandToExecute = instructions[index];
                    if (commandToExecute.command == "acc") {
                        total += int.Parse(commandToExecute.number);
                        index += 1;
                    } else if (commandToExecute.command == "jmp") {
                        index += int.Parse(commandToExecute.number);
                    } else if (commandToExecute.command == "nop") {
                        index += 1;
                    }
                    
                }
                if (pastIndexes.Contains(index)) {
                    total = 0;
                }
                return total;
            }

            int returnedValue = 0;

            foreach (Instruction step in instructions) {
                if (step.command == "jmp") {
                    step.command = "nop";
                    returnedValue = CheckLoop();
                    if (returnedValue != 0) {
                        break;
                    } else if (returnedValue == 0) { 
                        step.command = "jmp";
                    }
                } else if (step.command == "nop") {
                    step.command = "jmp";
                    returnedValue = CheckLoop();
                    if (returnedValue != 0) {
                        break;
                    } else if (returnedValue == 0) {
                        step.command = "nop";
                    }
                }
            }

            return returnedValue.ToString();
        }
    }
}
