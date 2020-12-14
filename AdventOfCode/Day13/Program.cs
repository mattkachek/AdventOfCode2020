using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day13
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
            var timestamp = int.Parse(inputs.First());

            var busIds = inputs.Last().Split(new char[] { ',' }).Where(b => b != "x").Select(s => int.Parse(s));

            var earliestBus = 0;
            var delay = 900;

            foreach(int busId in busIds)
            {
                if(busId - (timestamp % busId) < delay)
                {
                    delay = busId - (timestamp % busId);
                    earliestBus = busId;
                }
            }

            return (delay * earliestBus).ToString();
        }

        private string Part2()
        {
            var inputs = GetInput();
            var busIds = inputs.Last().Split(new char[] { ',' });
            var offsets = busIds.Select((b, i) => new Tuple<string, int>(b, i)).Where(o => o.Item1 !="x").Select(s => new Tuple<int, int>(int.Parse(s.Item1), s.Item2));

            long stepSize = 1;
            long answer = 1;
            foreach(var entry in offsets)
            {
                while((answer + entry.Item2) % entry.Item1 != 0)
                {
                    answer += stepSize;
                }
                stepSize *= entry.Item1;
            }

            return answer.ToString();
        }

        

        private List<string> GetInput()
        {
            var filePath = @"C:\Users\mkach\source\repos\AdventOfCode2020\AdventOfCode\Day13\Input.txt";
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
