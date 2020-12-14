using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day14
{
    public class Program
    {
        public void Select()
        {
            Console.WriteLine("Choose which version to run");
            Console.WriteLine("1 - Part 1");
            Console.WriteLine("2 - Part 2");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.WriteLine(Part1());
                    break;
                case "2":
                    Console.WriteLine(Part2());
                    break;

                default:
                    Console.WriteLine("Default Case");
                    break;
            }
        }

        private string Part1()
        {
            var inputs = GetInput();
            var instructionDict = new Dictionary<int, long>();
            var bitMask = "";

            foreach(string instruction in inputs)
            {
                var parsedInstruction = instruction.Split(new char[] { ' ' });
                var instType = parsedInstruction.First();
                var instValue = parsedInstruction.Last();

                if(instType == "mask")
                {
                    bitMask = instValue;
                }
                if(instType.StartsWith("mem"))
                {
                    var memAddr = int.Parse(instType.Substring(4).Replace("]", string.Empty));
                    var binaryString = Convert.ToString(int.Parse(instValue), 2).PadLeft(36, '0').ToCharArray();
                    for(int i=0; i<bitMask.Length; i++)
                    {
                        if(bitMask[i] != 'X')
                        {
                            binaryString[i] = bitMask[i];
                        }
                    }
                    instructionDict[memAddr] = Convert.ToInt64(new string(binaryString), 2);
                }
            }
            var sum = instructionDict.Sum(i => i.Value);

            return sum.ToString();
        }

        private string Part2()
        {
            var inputs = GetInput();
            var instructionDict = new Dictionary<long, long>();
            var bitMask = "";

            foreach (string instruction in inputs)
            {
                var parsedInstruction = instruction.Split(new char[] { ' ' });
                var instType = parsedInstruction.First();
                var instValue = parsedInstruction.Last();

                if (instType == "mask")
                {
                    bitMask = instValue;
                }
                if (instType.StartsWith("mem"))
                {
                    var memAddr = int.Parse(instType.Substring(4).Replace("]", string.Empty));
                    var binaryString = Convert.ToString(memAddr, 2).PadLeft(36, '0').ToCharArray();
                    var memAddrList = new List<char[]>();

                    memAddrList.Add(binaryString);

                    // Go through each character of bitmask and apply rule. 
                    // If character is 'X' then for each existing entry create both binary values from its next character and add them to the list
                    for (int i = 0; i < bitMask.Length; i++)
                    {
                        var tempAddrList = new List<char[]>();
                        foreach(char[] addr in memAddrList)
                        {
                            if (bitMask[i] == '0')
                            {
                                addr[i] = binaryString[i];
                            }
                            if (bitMask[i] == '1')
                            {
                                addr[i] = '1';
                            }
                            if (bitMask[i] == 'X')
                            {
                                var tempAddr = new char[36];
                                addr.CopyTo(tempAddr, 0);
                                addr[i] = '0';
                                tempAddr[i] = '1';
                                tempAddrList.Add(tempAddr);
                            }
                        }
                        memAddrList.AddRange(tempAddrList);
                    }
                    foreach(var addressToAdd in memAddrList)
                    {
                        instructionDict[Convert.ToInt64(new string(addressToAdd), 2)] = long.Parse(instValue);
                    }
                }
            }
            var sum = instructionDict.Sum(i => i.Value);

            return sum.ToString();
        }

        private List<string> GetInput()
        {
            var filePath = @"C:\Users\mkach\source\repos\AdventOfCode2020\AdventOfCode\Day14\Input.txt";
            List<string> values = new List<string>();

            using (StreamReader sr = File.OpenText(filePath))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    values.Add(s);
                }
            }
            return values;
        }
    }
}
