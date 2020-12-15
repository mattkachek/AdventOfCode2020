using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day15
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
            return FindValueForIteration(2020);
        }

        public string Part2()
        {
            return FindValueForIteration(30000000);
        }

        private string FindValueForIteration(int iteration)
        {
            var startingNumbers = GetInput().First().Split(new char[] { ',' }).Select(s => int.Parse(s)).ToArray();
            var StartNumCount = startingNumbers.Count();

            var gameValues = new Dictionary<int, int>();
            for (int i = 0; i < StartNumCount; i++)
            {
                gameValues.Add(startingNumbers[i], i);
            }

            var previousValue = startingNumbers.Last();
            var nextValue = 0;

            for (int i = StartNumCount; i < iteration; i++)
            {
                if (gameValues.ContainsKey(nextValue))
                {
                    previousValue = nextValue;
                    nextValue = i - gameValues[nextValue];
                    gameValues[previousValue] = i;
                }
                else
                {
                    gameValues[nextValue] = i;
                    previousValue = nextValue;
                    nextValue = 0;
                }
            }
            return gameValues.First(k => k.Value == (iteration-1)).Key.ToString();
        }

        private List<string> GetInput()
        {
            var filePath = @"C:\Users\mkach\source\repos\AdventOfCode2020\AdventOfCode\Day15\Input.txt";
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
