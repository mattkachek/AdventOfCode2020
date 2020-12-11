using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day10
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
                    Console.WriteLine(FindJoltageDiffs());
                    break;
                case "2":
                    Console.WriteLine(FindTotalArrangements());
                    break;

                default:
                    Console.WriteLine("Default Case");
                    break;
            }
        }

        private string FindJoltageDiffs()
        {
            var oneDiff = 0;
            var threeDiff = 1;
            var inputs = GetInput();
            inputs.Sort();

            if (inputs.ElementAt(0) == 1)
                oneDiff++;
            else if (inputs.ElementAt(0) == 3)
                threeDiff++;

            for (int i=1; i < inputs.Count(); i++)
            {
                var diff = inputs.ElementAt(i) - inputs.ElementAt(i-1);
                if (diff == 1)
                    oneDiff++;
                else if (diff == 3)
                    threeDiff++;
            }
            return (oneDiff * threeDiff).ToString();
        }

        private string FindTotalArrangements()
        {
            var inputs = GetInput();
            inputs.Add(0);
            inputs.Add(inputs.Max() + 3);
            inputs.Sort();

            List<List<int>> inputGroups = new List<List<int>>();

            double total = 1;
            var count = 0;

            // create groups of ints that start and end with required entries
            for(int i = 0; i < inputs.Count()-1; i++)
            {
                count++;
                if (inputs.ElementAt(i + 1) - inputs.ElementAt(i) == 3)
                {
                    inputGroups.Add(inputs.GetRange(i - count + 1, count));
                    count = 0;
                }
            }

            // find groups with optional entries to find number of valid combinations
            foreach(List<int> group in inputGroups)
            {
                var tempCount = 0;
                if(group.Count() > 2) // groups with potential combinations
                {
                    var firstlast = new List<int>();
                    firstlast.Add(group.First());
                    firstlast.Add(group.Last());
                    // Validate if first and last numbers are fine on their own
                    if(checkValidGroup(firstlast))
                    {
                        tempCount++;
                    }

                    // Get all combinations of inner values and the required outer values and validate them
                    var combos = GetAllCombos(group.GetRange(1, group.Count-2));
                    foreach(var combo in combos)
                    {
                        combo.Add(group.First());
                        combo.Add(group.Last());
                        combo.Sort();

                        if (checkValidGroup(combo))
                            tempCount++;
                    }
                    // Recombine sub groups into a total of possible arrangements
                    total = total * tempCount;
                }
            }

            
            return total.ToString();
        }

        private bool checkValidGroup(List<int> group)
        {
            for (int i = 1; i < group.Count; i++)
            {
                if (group.ElementAt(i) - group.ElementAt(i - 1) > 3)
                    return false;
            }
            return true;
        }

        // Got this from stack overflow to get all combinations. It just works.
        public static List<List<T>> GetAllCombos<T>(List<T> list)
        {
            List<List<T>> result = new List<List<T>>();
            // head
            result.Add(new List<T>());
            result.Last().Add(list[0]);
            if (list.Count == 1)
                return result;
            // tail
            List<List<T>> tailCombos = GetAllCombos(list.Skip(1).ToList());
            tailCombos.ForEach(combo =>
            {
                result.Add(new List<T>(combo));
                combo.Add(list[0]);
                result.Add(new List<T>(combo));
            });
            return result;
        }

        private List<int> GetInput()
        {
            var filePath = @"C:\Users\mkach\source\repos\AdventOfCode2020\AdventOfCode\Day10\Input.txt";
            List<int> values = new List<int>();

            using (StreamReader sr = File.OpenText(filePath))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    values.Add(int.Parse(s));
                }
            }
            return values;
        }
    }
}
