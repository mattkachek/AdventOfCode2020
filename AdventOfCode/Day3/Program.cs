using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day3
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
                    Console.WriteLine(Solve31Slope());
                    break;
                case "2":
                    Console.WriteLine(SolveMultipleSlopes());
                    break;

                default:
                    Console.WriteLine("Default Case");
                    break;
            }
        }

        private string Solve31Slope()
        {
            var map = GetInput();
            var treesHit = FindTrees(map, 3, 1);

            return treesHit.ToString();
        }

        private string SolveMultipleSlopes()
        {
            var map = GetInput();
            long answer = 1;

            answer = answer * FindTrees(map, 1, 1);
            answer = answer * FindTrees(map, 3, 1);
            answer = answer * FindTrees(map, 5, 1);
            answer = answer * FindTrees(map, 7, 1);
            answer = answer * FindTrees(map, 1, 2);

            return answer.ToString();
        }

        private int FindTrees(List<string> map, int xSlope, int ySlope)
        {
            var mapEnd = map.Count();
            var mapWidth = map.First().Length;
            var xCoor = 0;
            var yCoor = 0;
            var treesHit = 0;

            while(yCoor < mapEnd)
            {
                var mapValue = map.ElementAt(yCoor)[xCoor % mapWidth];
                if (mapValue == '#')
                    treesHit++;
                xCoor += xSlope;
                yCoor += ySlope;
            }

            return treesHit;
        }

        private List<string> GetInput()
        {
            var filePath = @"C:\Users\mkach\source\repos\AdventOfCode\AdventOfCode\Day3\Input.txt";
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
