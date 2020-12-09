using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day9
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
                    Console.WriteLine(FindInvalidEntry());
                    break;
                case "2":
                    Console.WriteLine(FindVulnerability());
                    break;

                default:
                    Console.WriteLine("Default Case");
                    break;
            }
        }

        private string FindInvalidEntry()
        {
            var inputs = GetInput();
            var startingIndex = 25;
            for(int i = startingIndex; i < inputs.Count(); i++)
            {
                var summedElement = new List<long>();
                for(int j = i-25; j<i-1; j++)
                {
                    for (int k = j; k < i; k++)
                    {
                        summedElement.Add(inputs.ElementAt(j) + inputs.ElementAt(k));
                    }
                }
                if (!summedElement.Contains(inputs.ElementAt(i)))
                {
                    return inputs.ElementAt(i).ToString();
                }
            }

            return "Not Found";
        }

        private string FindVulnerability()
        {
            var inputs = GetInput();
            var summedElement = 556543474;
            for (int i = 0; i < inputs.Count(); i++)
            {
                long tempTotal = 0;
                var values = new List<long>();
                for(int j = i; tempTotal < summedElement; j++)
                {
                    tempTotal += inputs.ElementAt(j);
                    values.Add(inputs.ElementAt(j));
                    if (tempTotal == summedElement)
                    {
                        return (values.Min() + values.Max()).ToString();
                    }
                }
            }

            return "Not Found";
        }

        private List<long> GetInput()
        {
            var filePath = @"C:\Users\mkach\source\repos\AdventOfCode2020\AdventOfCode\Day9\Input.txt";
            List<long> values = new List<long>();

            using (StreamReader sr = File.OpenText(filePath))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    values.Add(long.Parse(s));
                }
            }
            return values;
        }
    }
}
