using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day7
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
            Dictionary<string, List<InnerBag>> nodes = GetNodes(GetInput());

            List<string> containingBags = new List<string>();
            containingBags.Add("shiny gold");
            bool newBagFound = true;
            while(newBagFound)
            {
                newBagFound = false;
                foreach (KeyValuePair<string, List<InnerBag>> node in nodes)
                {
                    if(node.Value.Any(b => containingBags.Contains(b.color)) && !containingBags.Contains(node.Key))
                    {
                        containingBags.Add(node.Key);
                        newBagFound = true;
                    }
                }
            }


            return (containingBags.Count()-1).ToString();
        }

        private string Part2()
        {
            Dictionary<string, List<InnerBag>> nodes = GetNodes(GetInput());

            var total = FindBagTotal(nodes, "shiny gold").ToString();

            return total;
        }

        private int FindBagTotal(Dictionary<string, List<InnerBag>> bags, string startColor)
        {
            var currentTotal = 0;
            if(bags[startColor].Count() == 0)
            {
                return 0;
            }
            foreach (InnerBag bag in bags[startColor])
            {
                 currentTotal = currentTotal + bag.number +  bag.number * FindBagTotal(bags, bag.color);
            }
            return currentTotal;
        }

        private Dictionary<string, List<InnerBag>> GetNodes(List<string> rules)
        {
            Dictionary<string, List<InnerBag>> nodes = new Dictionary<string, List<InnerBag>>();
            foreach (string rule in rules)
            {
                var cleanedRule = rule.Replace("bags", string.Empty).Replace("bag", string.Empty).Replace(".", string.Empty);
                var parsedInput1 = cleanedRule.Split(new string[] { "contain" }, StringSplitOptions.RemoveEmptyEntries);
                var parsedInput2 = parsedInput1[1].Split(',');
                var childNodes = new List<InnerBag>();
                foreach (string child in parsedInput2)
                {
                    if (child.Contains("no other"))
                        continue;
                    var innerBag = new InnerBag(int.Parse(child.Trim()[0].ToString()), child.Trim().Substring(1).Trim());
                    childNodes.Add(innerBag);
                }

                nodes.Add(parsedInput1.First().Trim(), childNodes);
            }
            return nodes;
        }

        private List<string> GetInput()
        {
            var filePath = @"C:\Users\mkach\source\repos\AdventOfCode2020\AdventOfCode\Day7\Input.txt";
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

    public class InnerBag
    {
        public int number;
        public string color;

        public InnerBag(int num, string col)
        {
            number = num;
            color = col;
        }
    }
}
