using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day11
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
            var seatRows = GetInput();

            var seatsToChange = new List<Tuple<int, int>>();


            while (true)
            {

                seatsToChange = SeatsToOccupy(seatRows);

                foreach(Tuple<int, int> coord in seatsToChange)
                {
                    seatRows[coord.Item1][coord.Item2] = '#';
                }

                seatsToChange = SeatsToEmpty(seatRows);

                foreach (Tuple<int, int> coord in seatsToChange)
                {
                    seatRows[coord.Item1][coord.Item2] = 'L';
                }

                if (seatsToChange.Count == 0)
                    break;
            }

            return CountOccupiedSeats(seatRows).ToString();
        }

        private string Part2()
        {
            var seatRows = GetInput();

            var seatsToChange = new List<Tuple<int, int>>();
            
            while (true)
            {
                seatsToChange = SeatsToOccupyLong(seatRows);

                foreach (Tuple<int, int> coord in seatsToChange)
                {
                    seatRows[coord.Item1][coord.Item2] = '#';
                }

                seatsToChange = SeatsToEmptyLong(seatRows);

                foreach (Tuple<int, int> coord in seatsToChange)
                {
                    seatRows[coord.Item1][coord.Item2] = 'L';
                }

                if (seatsToChange.Count == 0)
                    break;
            }

            return CountOccupiedSeats(seatRows).ToString();
        }

        private int CheckSurrounding(char[][] seatRows, int x, int y)
        {
            var checkValue = '#';
            var counter = 0;

            for(int i= Math.Max(x-1,0); i<Math.Min(x+2, seatRows.Length); i++)
            {
                for(int j = Math.Max(y - 1, 0); j < Math.Min(y + 2, seatRows[0].Length); j++)
                {
                    if(i == x && j == y)
                    {
                        continue;
                    }
                    else
                    {
                        if (seatRows[i][j] == checkValue)
                            counter++;
                    }
                }
            }
            return counter;
        }

        private int CheckLongSurrounding(char[][] seatRows, int x, int y)
        {
            var counter = 0;

            // up left
            var tempx = x-1;
            var tempy = y-1;
            while(tempx>-1 && tempy>-1)
            {
                if(seatRows[tempx][tempy] == '#')
                {
                    counter++;
                    break;
                }
                if(seatRows[tempx][tempy] == 'L')
                {
                    break;
                }
                tempx--;
                tempy--;
            }

            //up
            tempx = x - 1;
            while (tempx > -1)
            {
                if (seatRows[tempx][y] == '#')
                {
                    counter++;
                    break;
                }
                if (seatRows[tempx][y] == 'L')
                {
                    break;
                }
                tempx--;
            }

            // up right
            tempx = x - 1;
            tempy = y + 1;
            while (tempx > -1 && tempy < seatRows[0].Length)
            {
                if (seatRows[tempx][tempy] == '#')
                {
                    counter++;
                    break;
                }
                if (seatRows[tempx][tempy] == 'L')
                {
                    break;
                }
                tempx--;
                tempy++;
            }

            //left
            tempy = y - 1;
            while (tempy > -1)
            {
                if (seatRows[x][tempy] == '#')
                {
                    counter++;
                    break;
                }
                if (seatRows[x][tempy] == 'L')
                {
                    break;
                }
                tempy--;
            }

            //right
            tempy = y + 1;
            while (tempy < seatRows[0].Length)
            {
                if (seatRows[x][tempy] == '#')
                {
                    counter++;
                    break;
                }
                if (seatRows[x][tempy] == 'L')
                {
                    break;
                }
                tempy++;
            }

            // down left
            tempx = x + 1;
            tempy = y - 1;
            while (tempx < seatRows.Length && tempy > -1)
            {
                if (seatRows[tempx][tempy] == '#')
                {
                    counter++;
                    break;
                }
                if (seatRows[tempx][tempy] == 'L')
                {
                    break;
                }
                tempx++;
                tempy--;
            }

            //down
            tempx = x + 1;
            while (tempx < seatRows.Length)
            {
                if (seatRows[tempx][y] == '#')
                {
                    counter++;
                    break;
                }
                if (seatRows[tempx][y] == 'L')
                {
                    break;
                }
                tempx++;
            }

            // down right
            tempx = x + 1;
            tempy = y + 1;
            while (tempx < seatRows.Length && tempy < seatRows[0].Length)
            {
                if (seatRows[tempx][tempy] == '#')
                {
                    counter++;
                    break;
                }
                if (seatRows[tempx][tempy] == 'L')
                {
                    break;
                }
                tempx++;
                tempy++;
            }

            return counter;
        }

        private List<Tuple<int, int>> SeatsToOccupy(char[][] seatRows)
        {
            var seatsToChange = new List<Tuple<int, int>>();
            for (int i = 0; i < seatRows.Count(); i++)
            {
                for (int j = 0; j < seatRows[0].Length; j++)
                {
                    if (seatRows[i][j] == 'L' && CheckSurrounding(seatRows, i, j) == 0)
                    {
                        seatsToChange.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            return seatsToChange;
        }

        private List<Tuple<int, int>> SeatsToEmpty(char[][] seatRows)
        {
            var seatsToChange = new List<Tuple<int, int>>();
            for (int i = 0; i < seatRows.Count(); i++)
            {
                for (int j = 0; j < seatRows[0].Length; j++)
                {
                    if (seatRows[i][j] == '#' && CheckSurrounding(seatRows, i, j) > 3)
                    {
                        seatsToChange.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            return seatsToChange;
        }

        private List<Tuple<int, int>> SeatsToOccupyLong(char[][] seatRows)
        {
            var seatsToChange = new List<Tuple<int, int>>();
            for (int i = 0; i < seatRows.Count(); i++)
            {
                for (int j = 0; j < seatRows[0].Length; j++)
                {
                    if (seatRows[i][j] == 'L' && CheckLongSurrounding(seatRows, i, j) == 0)
                    {
                        seatsToChange.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            return seatsToChange;
        }

        private List<Tuple<int, int>> SeatsToEmptyLong(char[][] seatRows)
        {
            var seatsToChange = new List<Tuple<int, int>>();
            for (int i = 0; i < seatRows.Count(); i++)
            {
                for (int j = 0; j < seatRows[0].Length; j++)
                {
                    if (seatRows[i][j] == '#' && CheckLongSurrounding(seatRows, i, j) > 4)
                    {
                        seatsToChange.Add(new Tuple<int, int>(i, j));
                    }
                }
            }
            return seatsToChange;
        }

        private int CountOccupiedSeats(char[][] seatRows)
        {
            var counter = 0;
            for (int i = 0; i < seatRows.Count(); i++)
            {
                for (int j = 0; j < seatRows[0].Length; j++)
                {
                    if (seatRows[i][j] == '#')
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        private char[][] GetInput()
        {
            var filePath = @"C:\Users\mkach\source\repos\AdventOfCode2020\AdventOfCode\Day11\Input.txt";
            List<string> values = new List<string>();

            using (StreamReader sr = File.OpenText(filePath))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    values.Add(s);
                }
            }
            return values.Select(s => s.ToArray()).ToArray();
        }
    }
}
