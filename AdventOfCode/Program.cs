using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose Day to Run");
            var day = Console.ReadLine();
            
            switch(day)
            {
                case "1":
                    var Day1 = new Day1.Program();
                    Day1.Select();
                    break;
                case "2":
                    var Day2 = new Day2.Program();
                    Day2.Select();
                    break;
                case "3":
                    var Day3 = new Day3.Program();
                    Day3.Select();
                    break;
                case "4":
                    var Day4 = new Day4.Program();
                    Day4.Select();
                    break;
                case "5":
                    var Day5 = new Day5.Program();
                    Day5.Select();
                    break;
                case "6":
                    var Day6 = new Day6.Program();
                    Day6.Select();
                    break;
                case "7":
                    var Day7 = new Day7.Program();
                    Day7.Select();
                    break;

                default:
                    Console.WriteLine("Default Case");
                    break;
            }

            Console.Write("\nPress any key to exit...");
            Console.ReadKey(true);

        }
    }
}
