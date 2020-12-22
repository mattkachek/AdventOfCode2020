using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day19
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

        public string Part1()
        {
            var fullInput = GetInput();
            var rulesInput = fullInput.TakeWhile(s => s!="");
            var messageInput = fullInput.SkipWhile(s => s != "").Skip(1);

            var ruleDict = new Dictionary<int, Tuple<string, string>>();
            foreach(string rule in rulesInput)
            {
                var ruleNum = int.Parse(rule.Split(':').First());
                var ruleOne = rule.Split(new char[] { ':', '|' })[1].Trim();
                var ruleTwo = "";
                if (rule.Contains('|'))
                {
                    ruleTwo = rule.Split(new char[] { ':', '|' })[2].Trim();
                }
                ruleDict.Add(ruleNum, new Tuple<string, string>(ruleOne, ruleTwo));
            }

            var matchCount = 0;
            var validMsgs = GetSubRuleStrings(ruleDict, 0);
            foreach(string message in messageInput)
            {
                if(validMsgs.Contains(message))
                {
                    matchCount++;
                }
            }

            return matchCount.ToString();
        }

        public string Part2()
        {
            var fullInput = GetInput();
            var rulesInput = fullInput.TakeWhile(s => s != "");
            var messageInput = fullInput.SkipWhile(s => s != "").Skip(1);

            var ruleDict = new Dictionary<int, Tuple<string, string>>();
            foreach (string rule in rulesInput)
            {
                var ruleNum = int.Parse(rule.Split(':').First());
                var ruleOne = rule.Split(new char[] { ':', '|' })[1].Trim();
                var ruleTwo = "";
                if (rule.Contains('|'))
                {
                    ruleTwo = rule.Split(new char[] { ':', '|' })[2].Trim();
                }
                ruleDict.Add(ruleNum, new Tuple<string, string>(ruleOne, ruleTwo));
            }

            var matchCount = 0;
            var validMsgs = GetSubRuleStrings(ruleDict, 0);
            var prefixes = GetSubRuleStrings(ruleDict, 42);
            var suffixes = GetSubRuleStrings(ruleDict, 31);
            var invalidMsgs = new List<string>();
            var doubleInvalidMsgs = new List<string>();

            foreach (string message in messageInput)
            {
                if (validMsgs.Contains(message))
                {
                    matchCount++;
                }
                else
                {
                    invalidMsgs.Add(message);
                }
            }

            foreach(string message in invalidMsgs)
            {
                var substrings = new List<string>();
                var stillValid = true;
                for(int i=0; i<message.Length; i+=8)
                {
                    substrings.Add(message.Substring(i, 8));
                }
                if (suffixes.Contains(substrings.Last()))
                {
                    while (stillValid && substrings.Count() > 2 && suffixes.Contains(substrings.Last()))
                    {
                        if (prefixes.Contains(substrings.First()))
                        {
                            substrings.RemoveAt(0);
                            substrings.RemoveAt(substrings.Count() - 1);
                        }
                        else
                        {
                            stillValid = false;
                        }

                    }
                    if (substrings.All(s => prefixes.Contains(s)))
                    {
                        matchCount++;
                    }
                }
            }

            return matchCount.ToString();
        }

        private List<string> GetSubRuleStrings(Dictionary<int, Tuple<string, string>> ruleDict, int ruleIndex)
        {
            var currentStringList = new List<string>();
            if(ruleDict[ruleIndex].Item1.Contains('\"'))
            {
                currentStringList.Add(ruleDict[ruleIndex].Item1.Replace("\"", string.Empty));
            }
            else
            {
                var paths1 = ruleDict[ruleIndex].Item1.Split(' ').Select(s => int.Parse(s)).ToList();
                var subList1 = GetSubRuleStrings(ruleDict, paths1[0]);
                var subList2 = new List<string>();
                subList2.Add("");
                if(paths1.Count() > 1)
                {
                    subList2 = GetSubRuleStrings(ruleDict, paths1[1]);
                }

                foreach(string sl1 in subList1)
                {
                    foreach(string sl2 in subList2)
                    {
                        currentStringList.Add(sl1 + sl2);
                    }
                }


                var paths2 = new List<int>();
                if (ruleDict[ruleIndex].Item2 != "")
                {
                    paths2 = ruleDict[ruleIndex].Item2.Split(' ').Select(s => int.Parse(s)).ToList();
                    subList1 = GetSubRuleStrings(ruleDict, paths2[0]);
                    subList2 = new List<string>();
                    subList2.Add("");
                    if (paths2.Count() > 1)
                    {
                        subList2 = GetSubRuleStrings(ruleDict, paths2[1]);
                    }

                    foreach (string sl1 in subList1)
                    {
                        foreach (string sl2 in subList2)
                        {
                            currentStringList.Add(sl1 + sl2);
                        }
                    }
                }

            }
            return currentStringList;
        }

        private List<string> GetInput()
        {
            var filePath = @"C:\Users\mkach\source\repos\AdventOfCode2020\AdventOfCode\Day19\Input.txt";
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
