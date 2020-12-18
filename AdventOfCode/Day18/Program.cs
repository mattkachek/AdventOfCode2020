using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day18
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
            var problems = GetInput();
            long total = 0;

            foreach(string problem in problems)
            {
                var simpleEquation = SolveAndReplaceParentheses(problem, false);
                var problemAnswer = SolveLeftToRight(simpleEquation);
                total += problemAnswer;
            }


            return total.ToString();
        }

        private string Part2()
        {
            var problems = GetInput();
            long total = 0;

            foreach (string problem in problems)
            {
                var simpleEquation = SolveAndReplaceParentheses(problem, true);
                var problemAnswer = SolveReversePrecedence(simpleEquation);
                total += problemAnswer;
            }


            return total.ToString();
        }

        private long SolveLeftToRight(string equation)
        {
            
            var equationParts = equation.Replace("(", string.Empty).Replace(")", string.Empty).Split(' ');
            long answer = long.Parse(equationParts.First());
            

            for(int i = 1; i<equationParts.Length; i+=2)
            {
                if(equationParts[i] == "+")
                {
                    answer += long.Parse(equationParts[i+1]);
                }
                else if (equationParts[i] == "*")
                {
                    answer *= long.Parse(equationParts[i+1]);
                }
            }

            return answer;
        }

        private long SolveReversePrecedence(string equation)
        {
            var equationParts = equation.Replace("(", string.Empty).Replace(")", string.Empty).Split(' ');

            
            while(equationParts.Contains("+"))
            {
                var baseIndex = Array.IndexOf(equationParts, "+");
                var subEquationAnswer = long.Parse(equationParts[baseIndex-1]) + long.Parse(equationParts[baseIndex+1]);

                equationParts[baseIndex] = subEquationAnswer.ToString();
                var equationPartsList = equationParts.ToList();
                equationPartsList.RemoveAt(baseIndex + 1);
                equationPartsList.RemoveAt(baseIndex - 1);
                equationParts = equationPartsList.ToArray();
            }
            while (equationParts.Contains("*"))
            {
                var baseIndex = Array.IndexOf(equationParts, "*");
                var subEquationAnswer = long.Parse(equationParts[baseIndex - 1]) * long.Parse(equationParts[baseIndex + 1]);

                equationParts[baseIndex] = subEquationAnswer.ToString();
                var equationPartsList = equationParts.ToList();
                equationPartsList.RemoveAt(baseIndex + 1);
                equationPartsList.RemoveAt(baseIndex - 1);
                equationParts = equationPartsList.ToArray();
            }

            return long.Parse(equationParts.First());
        }

        private string SolveAndReplaceParentheses(string equation, bool useReverse)
        {
            while(equation.Contains("("))
            {
                var end = equation.IndexOf(')');
                var start = equation.Substring(0, end).LastIndexOf("(");
                var subEquation = equation.Substring(start, end-start+1);
                if(!useReverse)
                {
                    equation = equation.Replace(subEquation, SolveLeftToRight(subEquation).ToString());
                }
                else
                {
                    equation = equation.Replace(subEquation, SolveReversePrecedence(subEquation).ToString());
                }
            }
            return equation;
        }

        private List<string> GetInput()
        {
            var filePath = @"C:\Users\mkach\source\repos\AdventOfCode2020\AdventOfCode\Day18\Input.txt";
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
