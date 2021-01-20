using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    class MemInputs {
        public string mask;
        public List<AddressAndValue> addVal = new List<AddressAndValue>();
    }

    class AddressAndValue {
        public int memAddress;
        public string bitValue;
    }

    class Day14 : ASolution
    {
        List<MemInputs> memInputs = new List<MemInputs>();

        public Day14() : base(14, 2020, "")
        {
            UseDebugInput = false;
            string[] SplitInput = Input.SplitByNewline();

            MemInputs maskForGroup = null;
            for (int i = 0; i < SplitInput.Length; i++) {
                string[] bits = SplitInput[i].Split(" = ");
                
                
                if (bits[0].Contains("mask")) { 
                    MemInputs newMem = new MemInputs();
                    newMem.mask = bits[1];
                    maskForGroup = newMem;
                    memInputs.Add(newMem);
                } else if (!bits[0].Contains("mask")) {
                    // insert loop here for each new mem address for a given mask
                    AddressAndValue newAdd = new AddressAndValue();
                    newAdd.memAddress = int.Parse(bits[0].Substring(4, bits[0].Length - 1/*ending bracket*/ - 4/*amount we skip*/));
                    newAdd.bitValue = Convert.ToString(long.Parse(bits[1]), 2);
                    newAdd.bitValue = newAdd.bitValue.PadLeft(36, '0');
                    maskForGroup.addVal.Add(newAdd);
                }
            }
        }

        protected override string SolvePartOne()
        {
            Dictionary<int, long> bitNumbers = new Dictionary<int, long>();

            foreach (var element in memInputs) {
                foreach (var address in element.addVal) {
                    char[] bitCharArray = address.bitValue.ToCharArray();
                    for (int i = 0; i < address.bitValue.Length; i++) {
                        if (element.mask[i] != 'X') {
                            if (element.mask[i] == '1') {
                                bitCharArray[i] = '1';
                            } else if (element.mask[i] == '0') {
                                bitCharArray[i] = '0';
                            }
                        }
                    }
                    bitNumbers[address.memAddress] = Convert.ToInt64(new string(bitCharArray), 2);
                }
            }

            return bitNumbers.Sum(kv => kv.Value).ToString();
        }

        protected override string SolvePartTwo()
        {
            Dictionary<long, long> bitNumbers = new Dictionary<long, long>();

            foreach (var element in memInputs) {
                List<string> masks = new List<string>();
                Queue<string> unprocessed = new Queue<string>();
                unprocessed.Enqueue(element.mask.Replace('0', '#'));

                while (unprocessed.Count > 0) {
                    char[] maskItem = unprocessed.Dequeue().ToCharArray();
                    int i = Array.IndexOf(maskItem, 'X');
                    if (i != -1) {
                        maskItem[i] = '1';
                        unprocessed.Enqueue(new string(maskItem));
                        maskItem[i] = '0';
                        unprocessed.Enqueue(new string(maskItem));
                    } else if (i == -1) {
                        masks.Add(new string(maskItem));
                    }
                }

                foreach (var address in element.addVal) {
                    foreach (string bitMask in masks) { 
                        char[] bitAddress = (Convert.ToString(address.memAddress, 2)).PadLeft(36, '0').ToCharArray();
                        for (int i = 0; i < bitMask.Length; i++) {
                            if (bitMask[i] == '1') {
                                bitAddress[i] = '1';
                            } else if (bitMask[i] == '0') {
                                bitAddress[i] = '0';
                            }
                        }
                    bitNumbers[Convert.ToInt64(new string(bitAddress), 2)] = Convert.ToInt64(new string(address.bitValue), 2);
                    }
                }
            }

            return bitNumbers.Sum(kv => kv.Value).ToString();
        }
    }
}
