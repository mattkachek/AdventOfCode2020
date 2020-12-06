using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode.Day4
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
                    Console.WriteLine(SolveSimpleValidation());
                    break;
                case "2":
                    Console.WriteLine(SolveComplexValidation());
                    break;

                default:
                    Console.WriteLine("Default Case");
                    break;
            }
        }

        private string SolveSimpleValidation()
        {
            var input = GetInput();
            var totalValidPassports = 0;
            string[] requiredFields = { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid"};

            foreach(string passport in input)
            {
                if (requiredFields.All(p => passport.Contains(p)))
                {
                    totalValidPassports++;
                }
            }

            return totalValidPassports.ToString();
        }

        private string SolveComplexValidation()
        {
            var input = GetInput();
            var totalValidPassports = 0;
            string[] requiredFields = { "byr", "iyr", "eyr", "hgt", "hcl", "ecl", "pid" };

            foreach (string passport in input)
            {
                if (requiredFields.All(p => passport.Contains(p)))
                {
                    if(IsValidPassport(passport))
                    {
                        totalValidPassports++;
                    }
                }
            }

            return totalValidPassports.ToString();
        }

        private bool IsValidPassport(string passport)
        {
            var passportDict = new Dictionary<string, string>();
            string[] strSplit = { " ", "\r\n" };
            foreach (string entry in passport.Split(strSplit, StringSplitOptions.RemoveEmptyEntries))
            {
                var kvp = entry.Split(':');
                passportDict.Add(kvp[0], kvp[1]);
            }

            if (passportDict["byr"].Length != 4 || int.Parse(passportDict["byr"]) < 1920 || int.Parse(passportDict["byr"]) > 2002)
                return false;
            if (passportDict["iyr"].Length != 4 || int.Parse(passportDict["iyr"]) < 2010 || int.Parse(passportDict["iyr"]) > 2020)
                return false;
            if (passportDict["eyr"].Length != 4 || int.Parse(passportDict["eyr"]) < 2020 || int.Parse(passportDict["eyr"]) > 2030)
                return false;

            if(!Regex.IsMatch(passportDict["hgt"], "^([0-9]{3}[c][m]|[0-9]{2}[i][n])$"))
            {
                return false;
            }
            if (Regex.IsMatch(passportDict["hgt"], "^([0-9]{3}[c][m])$") && (int.Parse(passportDict["hgt"].Substring(0, 3)) < 150 || int.Parse(passportDict["hgt"].Substring(0, 3)) > 193))
                return false;
            if (Regex.IsMatch(passportDict["hgt"], "^([0-9]{2}[i][n])$") && (int.Parse(passportDict["hgt"].Substring(0, 2)) < 59 || int.Parse(passportDict["hgt"].Substring(0, 2)) > 76))
                return false;

            if (!Regex.IsMatch(passportDict["hcl"], "^[#][a-f0-9]{6}$"))
                return false;
            string[] eyeColors = { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            if (!eyeColors.Contains(passportDict["ecl"]))
                return false;
            if (!Regex.IsMatch(passportDict["pid"], "^[0-9]{9}$"))
                return false;

            return true;
        }

        private List<string> GetInput()
        {
            var filePath = @"C:\Users\mkach\source\repos\AdventOfCode2020\AdventOfCode\Day4\Input.txt";
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
    }
}
