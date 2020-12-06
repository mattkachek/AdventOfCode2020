using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day6
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
                    Console.WriteLine(SumYesAnswers());
                    break;
                case "2":
                    Console.WriteLine(SumYesAnswersForGroups());
                    break;

                default:
                    Console.WriteLine("Default Case");
                    break;
            }
        }

        private string SumYesAnswers()
        {
            var input = GetInput();
            var answerTotal = 0;

            foreach(string group in input)
            {
                Console.WriteLine(group);
                answerTotal += group.Distinct().Count(char.IsLetter);
            }

            return answerTotal.ToString();
        }

        private string SumYesAnswersForGroups()
        {
            var input = GetInputAsGroups();
            var answerTotal = 0;

            foreach (List<string> group in input)
            {
                var start = group.First().ToList();
                foreach(string answers in group)
                {
                    start = start.Intersect(answers).ToList();
                }
                answerTotal += start.Count();
            }

            return answerTotal.ToString();
        }

        private List<string> GetInput()
        {
            var filePath = @"C:\Users\mkach\source\repos\AdventOfCode2020\AdventOfCode\Day6\Input.txt";
            string fullInput = "";
            List<string> values = new List<string>();

            using (StreamReader sr = File.OpenText(filePath))
            {
                fullInput = sr.ReadToEnd();
            }
            //Console.WriteLine(Regex.Escape(fullInput.Substring(0, 200)));

            string[] delimiter = { "\r\n\r\n" };
            values = fullInput.Split(delimiter, StringSplitOptions.RemoveEmptyEntries).ToList();
            //Console.WriteLine(values.First());

            return values;
        }

        private List<List<string>> GetInputAsGroups()
        {
            var filePath = @"C:\Users\mkach\source\repos\AdventOfCode2020\AdventOfCode\Day6\Input.txt";
            string fullInput = "";
            List<List<string>> values = new List<List<string>>();

            using (StreamReader sr = File.OpenText(filePath))
            {
                fullInput = sr.ReadToEnd();
            }
            //Console.WriteLine(Regex.Escape(fullInput.Substring(0, 200)));

            string[] delimiter = { "\r\n\r\n" };
            var fullFile = fullInput.Split(delimiter, StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach(string group in fullFile)
            {
                List<string> fullGroup = new List<string>();
                string[] nl = { "\r\n" };
                foreach (string s in group.Split(nl, StringSplitOptions.RemoveEmptyEntries))
                {
                    fullGroup.Add(s);
                }
                values.Add(fullGroup);
            }
            //Console.WriteLine(values.First());

            return values;
        }
    }
}
