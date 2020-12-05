using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day2
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
                    Console.WriteLine(CheckValidPasswordsSled());
                    break;
                case "2":
                    Console.WriteLine(CheckValidPasswordsToboggan());
                    break;

                default:
                    Console.WriteLine("Default Case");
                    break;
            }
        }

        private string CheckValidPasswordsSled()
        {
            char[] delimiterChars = { ' ', '-', ':'};
            var validCount = 0;
            List<string> values = GetInput();
            foreach (string value in values)
            {
                string[] passwordParams = value.Split(delimiterChars);
                var password = new PassObj(passwordParams[4], passwordParams[2], int.Parse(passwordParams[0]), int.Parse(passwordParams[1]));
                if (IsPasswordValidSled(password))
                    validCount++;
            }

            return validCount.ToString();
        }

        private string CheckValidPasswordsToboggan()
        {
            char[] delimiterChars = { ' ', '-', ':' };
            var validCount = 0;
            List<string> values = GetInput();
            foreach (string value in values)
            {
                string[] passwordParams = value.Split(delimiterChars);
                var password = new PassObj(passwordParams[4], passwordParams[2], int.Parse(passwordParams[0]), int.Parse(passwordParams[1]));
                if (IsPasswordValidToboggan(password))
                    validCount++;
            }

            return validCount.ToString();
        }

        private List<string> GetInput()
        {
            var filePath = @"C:\Users\mkach\source\repos\AdventOfCode\AdventOfCode\Day2\Input.txt";
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

        private bool IsPasswordValidSled(PassObj passObj)
        {
            var letterCount = passObj.password.Count(s => s.ToString() == passObj.requiredLetter);
            if(letterCount >= passObj.lowerBound && letterCount <= passObj.upperBound)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsPasswordValidToboggan(PassObj passObj)
        {
            bool firstValue = passObj.password[passObj.lowerBound-1].ToString() == passObj.requiredLetter;
            bool secondValue = passObj.password[passObj.upperBound-1].ToString() == passObj.requiredLetter;
            if (firstValue ^ secondValue)
                return true;
            else
                return false;
        }
    }


    class PassObj
    {
        public string password;
        public string requiredLetter;
        public int lowerBound;
        public int upperBound;

        public PassObj(string _password, string _requiredLetter, int _lowerBound, int _upperBound)
        {
            password = _password;
            requiredLetter = _requiredLetter;
            lowerBound = _lowerBound;
            upperBound = _upperBound;
        }
    }
}
