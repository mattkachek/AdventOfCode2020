using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day12
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
            var actions = GetInput();

            var northsouth = 0;
            var eastwest = 0;
            var facing = Facing.East;

            foreach(string action in actions)
            {
                var dir = action[0];
                var amount = int.Parse(action.Substring(1));
                if (dir == 'R')
                {
                    facing = (Facing)(((int)facing + (amount / 90)) % 4);
                }
                else if (dir == 'L')
                {
                    var facingTemp = ((int)facing - (amount / 90)) % 4;
                    while (facingTemp < 0)
                        facingTemp += 4;
                    facing = (Facing)facingTemp;
                }
                else if (dir == 'F')
                {
                    if (facing == Facing.North)
                    {
                        northsouth += amount;
                    }
                    else if (facing == Facing.South)
                    {
                        northsouth -= amount;
                    }
                    else if (facing == Facing.East)
                    {
                        eastwest += amount;
                    }
                    else if (facing == Facing.West)
                    {
                        eastwest -= amount;
                    }
                }
                else if (dir == 'N')
                {
                    northsouth += amount;
                }
                else if (dir == 'S')
                {
                    northsouth -= amount;
                }
                else if (dir == 'E')
                {
                    eastwest += amount;
                }
                else if (dir == 'W')
                {
                    eastwest -= amount;
                }
                else
                    Console.WriteLine("Error with value: " + dir);

            }
            
            return GetManhattanDistance(northsouth, eastwest).ToString();
        }

        private string Part2()
        {
            var actions = GetInput();

            var northsouth = 0;
            var eastwest = 0;
            var waypointNS = 1;
            var waypointEW = 10;

            foreach (string action in actions)
            {
                var dir = action[0];
                var amount = int.Parse(action.Substring(1));
                if (dir == 'R')
                {
                    var count = amount / 90;
                    while (count > 0)
                    {
                        var temp = waypointEW;
                        waypointEW = waypointNS;
                        waypointNS = -temp;
                        count--;
                    }
                }
                else if (dir == 'L')
                {
                    var count = amount / 90;
                    while (count > 0)
                    {
                        var temp = waypointEW;
                        waypointEW = -waypointNS;
                        waypointNS = temp;
                        count--;
                    }
                }
                else if (dir == 'F')
                {
                    northsouth += amount * waypointNS;
                    eastwest += amount * waypointEW;
                }
                else if (dir == 'N')
                {
                    waypointNS += amount;
                }
                else if (dir == 'S')
                {
                    waypointNS -= amount;
                }
                else if (dir == 'E')
                {
                    waypointEW += amount;
                }
                else if (dir == 'W')
                {
                    waypointEW -= amount;
                }
                else
                    Console.WriteLine("Error with value: " + dir);

            }

            return GetManhattanDistance(northsouth, eastwest).ToString();
        }

        private int GetManhattanDistance(int value1, int value2)
        {
            return Math.Abs(value1) + Math.Abs(value2);
        }

        private List<string> GetInput()
        {
            var filePath = @"C:\Users\mkach\source\repos\AdventOfCode2020\AdventOfCode\Day12\Input.txt";
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

        private enum Facing
        {
            North = 0,
            East = 1,
            South = 2,
            West = 3
        }
    }
}
