using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day5
{
    class Program
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
                    Console.WriteLine(FindHighestId());
                    break;
                case "2":
                    Console.WriteLine(FindMySeat());
                    break;

                default:
                    Console.WriteLine("Default Case");
                    break;
            }
        }

        private string FindHighestId()
        {
            var highestId = 0;
            var boardingPasses = GetInput();

            foreach(string bp in boardingPasses)
            {
                var rowString = bp.Substring(0, 7);
                var colString = bp.Substring(7, 3);

                var rowBinary = rowString.Replace('F', '0').Replace('B','1');
                var rowVal = Convert.ToInt32(rowBinary, 2);
                var colBinary = colString.Replace('L', '0').Replace('R', '1');
                var colVal = Convert.ToInt32(colBinary, 2);
                // Console.WriteLine(Convert.ToInt32(rowBinary, 2));
                if (rowVal * 8 + colVal > highestId)
                    highestId = rowVal * 8 + colVal;

            }

            return highestId.ToString();
        }

        private string FindMySeat()
        {
            var mySeatId = 0;
            var seatIds = new List<int>();

            var boardingPasses = GetInput();

            foreach (string bp in boardingPasses)
            {
                var rowString = bp.Substring(0, 7);
                var colString = bp.Substring(7, 3);

                var rowBinary = rowString.Replace('F', '0').Replace('B', '1');
                var rowVal = Convert.ToInt32(rowBinary, 2);
                var colBinary = colString.Replace('L', '0').Replace('R', '1');
                var colVal = Convert.ToInt32(colBinary, 2);
                seatIds.Add(rowVal * 8 + colVal);
            }

            seatIds.Sort();

            foreach(int seat in seatIds)
            {
                if (!seatIds.Contains(seat - 1))
                    mySeatId = seat-1;
            }

            return mySeatId.ToString();
        }

        private List<string> GetInput()
        {
            var filePath = @"C:\Users\mkach\source\repos\AdventOfCode\AdventOfCode\Day5\Input.txt";
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
