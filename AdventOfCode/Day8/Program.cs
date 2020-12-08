using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day8
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
                    Console.WriteLine(FindAccumulatorAtLoop());
                    break;
                case "2":
                    Console.WriteLine(FixProgram());
                    break;

                default:
                    Console.WriteLine("Default Case");
                    break;
            }
        }

        private string FindAccumulatorAtLoop()
        {
            var instructions = GetInput();
            var accumulator = 0;
            var runInsturctions = new List<int>();


            for(int i=0; i<instructions.Count(); i++)
            {
                if(runInsturctions.Contains(i))
                {
                    return accumulator.ToString();
                }
                var instruction = instructions.ElementAt(i);
                if(instruction.Contains("acc"))
                {
                    accumulator += int.Parse(instruction.Split(' ').Last());
                }
                if(instruction.Contains("jmp"))
                {
                    i = i + int.Parse(instruction.Split(' ').Last()) -1;
                }
                runInsturctions.Add(i);
            }

            return "Reached End";
        }

        private string FixProgram()
        {
            var instructions = GetInput();

            for(int j=0; j< instructions.Count(); j++)
            {
                if (instructions.ElementAt(j).Contains("acc"))
                {
                    continue;
                }

                var accumulator = 0;
                var runInsturctions = new List<int>();
            
                for (int i = 0; i != instructions.Count(); i++)
                {
                    if (runInsturctions.Contains(i))
                    {
                        accumulator = -1;
                        break;
                    }

                    var instruction = instructions.ElementAt(i);
                    if(i == j)
                    {
                        if (instruction.Contains("jmp"))
                        {
                            continue;
                        }
                        if (instruction.Contains("nop"))
                        {
                            i = i + int.Parse(instruction.Split(' ').Last()) - 1;
                        }
                    }
                    else
                    {
                        if (instruction.Contains("acc"))
                        {
                            accumulator += int.Parse(instruction.Split(' ').Last());
                        }
                        if (instruction.Contains("jmp"))
                        {
                            i = i + int.Parse(instruction.Split(' ').Last()) - 1;
                        }
                    }
                    
                    runInsturctions.Add(i);

                }

                if(accumulator != -1)
                    return accumulator.ToString();
            }
            return "None Found";
        }

        private List<string> GetInput()
        {
            var filePath = @"C:\Users\mkach\source\repos\AdventOfCode2020\AdventOfCode\Day8\Input.txt";
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
