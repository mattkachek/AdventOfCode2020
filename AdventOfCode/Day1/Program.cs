using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AdventOfCode.Day1
{
    public class Program
    {
        public void Select()
        {
            Console.WriteLine("Choose which version to run");
            Console.WriteLine("1 - For two numbers");
            Console.WriteLine("2 - For three numbers");
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.WriteLine(SolveFor2());
                    break;
                case "2":
                    Console.WriteLine(SolveFor3());
                    break;

                default:
                    Console.WriteLine("Default Case");
                    break;
            }
        }


        public string SolveFor2()
        {
            var filePath = @"C:\Users\mkach\source\repos\AdventOfCode2020\AdventOfCode\Day1\Input.txt";
            List<int> values = new List<int>();

            if (!File.Exists(filePath))
            { 
                return "File not found at location: " + filePath;
            }

            using (StreamReader sr = File.OpenText(filePath))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    // Console.WriteLine(s);
                    values.Add(Int32.Parse(s));
                }
            }

            for(int i=0; i<values.Count; i++)
            {
                for(int j=i; j<values.Count; j++)
                {
                    var value1 = values.ElementAt(i);
                    var value2 = values.ElementAt(j);
                    if (value1 + value2 == 2020)
                    {
                        var finalAnswer = value1 * value2;
                        return finalAnswer.ToString();
                    }
                }
            }

            return "No Answer Found";
        }

        public string SolveFor3()
        {
            var filePath = @"C:\Users\mkach\source\repos\AdventOfCode\AdventOfCode\Day1\Input.txt";
            List<int> values = new List<int>();

            if (!File.Exists(filePath))
            {
                return "File not found at location: " + filePath;
            }

            using (StreamReader sr = File.OpenText(filePath))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    // Console.WriteLine(s);
                    values.Add(Int32.Parse(s));
                }
            }

            for (int i = 0; i < values.Count; i++)
            {
                for (int j = i; j < values.Count; j++)
                {
                    for (int k = j; k < values.Count; k++)
                    {
                        var value1 = values.ElementAt(i);
                        var value2 = values.ElementAt(j);
                        var value3 = values.ElementAt(k);
                        if (value1 + value2 + value3 == 2020)
                        {
                            var finalAnswer = value1 * value2 * value3;
                            return finalAnswer.ToString();
                        }

                    }
                }
            }

            return "No Answer Found";
        }
    }
}
