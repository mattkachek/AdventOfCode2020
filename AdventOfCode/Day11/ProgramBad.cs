using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day11
{
    //This code has been commented out to stop annoying namespace issues with the classes inside of it
    //This code is only left in for posterity and was abandoned in favor of the alternative solution implemented

    public class ProgramBad
    {
    //    public void Select()
    //    {
    //        Console.WriteLine("Choose which version to run");
    //        Console.WriteLine("1 - Part 1");
    //        Console.WriteLine("2 - Part 2");
    //        var input = Console.ReadLine();
    //        switch (input)
    //        {
    //            case "1":
    //                Console.WriteLine(Part1());
    //                break;
    //            case "2":
    //                Console.WriteLine(Part2());
    //                break;

    //            default:
    //                Console.WriteLine("Default Case");
    //                break;
    //        }
    //    }

    //    private string Part1()
    //    {
    //        var seatRows = GetInput();
    //        var map = new SeatMap(seatRows);


    //        return "";
    //    }

    //    private string Part2()
    //    {

    //        return "";
    //    }

    //    private List<string> GetInput()
    //    {
    //        var filePath = @"C:\Users\mkach\source\repos\AdventOfCode2020\AdventOfCode\Day11\Input.txt";
    //        List<string> values = new List<string>();

    //        using (StreamReader sr = File.OpenText(filePath))
    //        {
    //            string s;
    //            while ((s = sr.ReadLine()) != null)
    //            {
    //                values.Add(s);
    //            }
    //        }
    //        return values;
    //    }
    }

    //public class SeatMap
    //{
    //    public Seat[,] map;

    //    public SeatMap(List<string> seatingList)
    //    {
    //        var rowLength = seatingList.First().Length;
    //        map = new Seat[seatingList.Count(), rowLength];

    //        foreach(string row in seatingList)
    //        {
    //            var rowIndex = 0;
    //            foreach(char seat in row)
    //            {
    //                if (seat == 'L')
    //                {
    //                    map[seatingList.IndexOf(row), rowIndex] = new Seat(SeatCondition.Empty);
    //                }
    //                if (seat == '.')
    //                {
    //                    map[seatingList.IndexOf(row), rowIndex] = new Seat(SeatCondition.Floor);
    //                }
    //                rowIndex++;
    //            }
    //        }
    //    }

    //    public void OccupySeats()
    //    {
    //        for(int i=0; i<map.GetLength(0); i++)
    //        {
    //            for(int j=0; j<map.GetLength(1); j++)
    //            {
    //                if(!CheckSurroundingSeats(SeatCondition.Occupied,0,i,j))
    //                {
    //                    map[i, j].shouldChange = true;
    //                }
    //            }
    //        }
    //    }

    //    public void ChangeSeats()
    //    {
    //        foreach (Seat seat in map)
    //        {
    //            if (seat.shouldChange)
    //            {
    //                seat.Change();
    //            }
    //        }
    //    }

    //    public bool CheckSurroundingSeats(SeatCondition checkValue, int checkTotal, int x, int y)
    //    {
    //        var slots = new Seat[]{map[x-1,y-1], map[x-1, y], map[x-1, y+1],
    //                                map[x,y-1], map[x,y+1],
    //                                map[x+1,y-1], map[x+1,y], map[x+1,y+1]};

    //        var counter = 0;
    //        foreach(Seat seat in slots)
    //        {
    //            if (seat.seatCondition == checkValue)
    //            {
    //                counter++;
    //            }
    //        }
    //        if(counter > checkTotal)
    //        {
    //            return true;
    //        }
    //        return false;
    //    }

    //    public int TotalOccupied()
    //    {
    //        var counter = 0;
    //        foreach(Seat seat in map)
    //        {
    //            if(seat.seatCondition == SeatCondition.Occupied)
    //            {
    //                counter++;
    //            }
    //        }
    //        return counter;
    //    }

    //}

    //public class Seat
    //{
    //    public SeatCondition seatCondition;
    //    public bool shouldChange;

    //    public Seat(SeatCondition _seatCondition)
    //    {
    //        seatCondition = _seatCondition;
    //        shouldChange = false;
    //    }

    //    public void Change()
    //    {
    //        switch(seatCondition)
    //        {
    //            case SeatCondition.Empty:
    //                seatCondition = SeatCondition.Floor;
    //                break;
    //            case SeatCondition.Occupied:
    //                seatCondition = SeatCondition.Empty;
    //                break;
    //            default:
    //                break;
    //        }
    //        shouldChange = false;
    //    }
    //}

    //public enum SeatCondition
    //{
    //    Empty,
    //    Occupied,
    //    Floor
    //}
}
