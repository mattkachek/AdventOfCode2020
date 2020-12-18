using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Day17
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
            var inputState = GetInput();
            var xSize = 20;
            var ySize = 20;
            var zSize = 13;
            var cubeArray = new Cube[xSize, ySize, zSize];

            //initialize array for type
            for(int i=0; i<xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    for (int k = 0; k < zSize; k++)
                    {
                        cubeArray[i, j, k] = new Cube();
                    }
                }
            }

            //Set initial active cubes
            for(int i=0; i < inputState.Count(); i++)
            {
                for(int j = 0; j<inputState.First().Count(); j++)
                {
                    if(inputState.ElementAt(i)[j] == '#')
                    {
                        cubeArray[i + 6, j + 6, 6].isActive = true;
                    }
                }
            }

            var totalIterations = 6;
            var minToCheck = new Tuple<int, int, int>(5, 5, 5);
            var maxToCheck = new Tuple<int, int, int>(14, 14, 7);
            for(int i = 0; i < totalIterations; i++)
            {
                for(int j = minToCheck.Item1; j<maxToCheck.Item1+1; j++)
                {
                    for(int k = minToCheck.Item2; k<maxToCheck.Item2+1; k++)
                    {
                        for(int l=minToCheck.Item3; l<maxToCheck.Item3+1; l++)
                        {
                            var currentCube = cubeArray[j, k, l];
                            var activeSurrounding = CheckSurroundingCubes(cubeArray, new Tuple<int, int, int>(j, k, l));
                            if(currentCube.isActive && !(activeSurrounding == 2 || activeSurrounding == 3))
                            {
                                currentCube.SetShouldChange();
                            }
                            else if(!currentCube.isActive && activeSurrounding == 3)
                            {
                                currentCube.SetShouldChange();
                            }
                        }
                    }

                    Print2dSlice(cubeArray);
                }
                foreach(Cube cube in cubeArray)
                {
                    cube.ChangeState();
                }

                minToCheck = new Tuple<int, int, int>(minToCheck.Item1 - 1, minToCheck.Item2 - 1, minToCheck.Item3 - 1);
                maxToCheck = new Tuple<int, int, int>(maxToCheck.Item1 + 1, maxToCheck.Item2 + 1, maxToCheck.Item3 + 1);
            }

            var finalActiveCount = 0;
            foreach(Cube cube in cubeArray)
            {
                if(cube.isActive)
                {
                    finalActiveCount++;
                }
            }

            return finalActiveCount.ToString();
        }

        private string Part2()
        {
            var inputState = GetInput();
            var xSize = 20;
            var ySize = 20;
            var zSize = 13;
            var wsize = 13;
            var cubeArray = new Cube[xSize, ySize, zSize, wsize];

            //initialize array for type
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    for (int k = 0; k < zSize; k++)
                    {
                        for (int l = 0; l < zSize; l++)
                        {
                            cubeArray[i, j, k, l] = new Cube();
                        }
                    }
                }
            }

            //Set initial active cubes
            for (int i = 0; i < inputState.Count(); i++)
            {
                for (int j = 0; j < inputState.First().Count(); j++)
                {
                    if (inputState.ElementAt(i)[j] == '#')
                    {
                        cubeArray[i + 6, j + 6, 6, 6].isActive = true;
                    }
                }
            }

            var totalIterations = 6;
            var minToCheck = new Tuple<int, int, int, int>(5, 5, 5, 5);
            var maxToCheck = new Tuple<int, int, int, int>(14, 14, 7, 7);
            for (int i = 0; i < totalIterations; i++)
            {
                for (int j = minToCheck.Item1; j < maxToCheck.Item1 + 1; j++)
                {
                    for (int k = minToCheck.Item2; k < maxToCheck.Item2 + 1; k++)
                    {
                        for (int l = minToCheck.Item3; l < maxToCheck.Item3 + 1; l++)
                        {
                            for (int m = minToCheck.Item3; m < maxToCheck.Item3 + 1; m++)
                            {
                                var currentCube = cubeArray[j, k, l, m];
                                var activeSurrounding = CheckSurroundingCubes(cubeArray, new Tuple<int, int, int, int>(j, k, l, m));
                                if (currentCube.isActive && !(activeSurrounding == 2 || activeSurrounding == 3))
                                {
                                    currentCube.SetShouldChange();
                                }
                                else if (!currentCube.isActive && activeSurrounding == 3)
                                {
                                    currentCube.SetShouldChange();
                                }
                            }
                        }
                    }
                }
                foreach (Cube cube in cubeArray)
                {
                    cube.ChangeState();
                }

                minToCheck = new Tuple<int, int, int, int>(minToCheck.Item1 - 1, minToCheck.Item2 - 1, minToCheck.Item3 - 1, minToCheck.Item4 -1);
                maxToCheck = new Tuple<int, int, int, int>(maxToCheck.Item1 + 1, maxToCheck.Item2 + 1, maxToCheck.Item3 + 1, maxToCheck.Item4 + 1);
            }

            var finalActiveCount = 0;
            foreach (Cube cube in cubeArray)
            {
                if (cube.isActive)
                {
                    finalActiveCount++;
                }
            }

            return finalActiveCount.ToString();
        }

        private void Print2dSlice(Cube[,,] cube)
        {
            for(int i=0; i<20; i++)
            {
                var myLine = "";
                for(int j=0; j<20; j++)
                {
                    myLine = myLine + (cube[i, j, 6].isActive ? "1" : "0");
                }
                Console.WriteLine(myLine);
            }
        }

        private int CheckSurroundingCubes(Cube[,,] cubeArray, Tuple<int, int, int> position)
        {
            var totalActive = 0;

            for(int i=Math.Max(position.Item1-1, 0); i<Math.Min(position.Item1+2, cubeArray.GetLength(0)); i++)
            {
                for (int j = Math.Max(position.Item2 - 1, 0); j < Math.Min(position.Item2 + 2, cubeArray.GetLength(1)); j++)
                {
                    for (int k = Math.Max(position.Item3 - 1, 0); k < Math.Min(position.Item3 + 2, cubeArray.GetLength(2)); k++)
                    {
                        if(!(i==position.Item1 && j==position.Item2 && k==position.Item3))
                        {
                            if(cubeArray[i,j,k].isActive)
                            {
                                totalActive++;
                            }
                        }
                    }
                }
            }

            return totalActive;
        }

        private int CheckSurroundingCubes(Cube[,,,] cubeArray, Tuple<int, int, int, int> position)
        {
            var totalActive = 0;

            for (int i = Math.Max(position.Item1 - 1, 0); i < Math.Min(position.Item1 + 2, cubeArray.GetLength(0)); i++)
            {
                for (int j = Math.Max(position.Item2 - 1, 0); j < Math.Min(position.Item2 + 2, cubeArray.GetLength(1)); j++)
                {
                    for (int k = Math.Max(position.Item3 - 1, 0); k < Math.Min(position.Item3 + 2, cubeArray.GetLength(2)); k++)
                    {
                        for (int l = Math.Max(position.Item4 - 1, 0); l < Math.Min(position.Item4 + 2, cubeArray.GetLength(3)); l++)
                        {
                            if (!(i == position.Item1 && j == position.Item2 && k == position.Item3 && l ==position.Item4))
                            {
                                if (cubeArray[i, j, k, l].isActive)
                                {
                                    totalActive++;
                                }
                            }
                        }
                    }
                }
            }

            return totalActive;
        }

        private List<string> GetInput()
        {
            var filePath = @"C:\Users\mkach\source\repos\AdventOfCode2020\AdventOfCode\Day17\Input.txt";
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

        private class Cube
        {
            public bool isActive;
            bool shouldChange;

            public Cube()
            {
                isActive = false;
                shouldChange = false;
            }

            public Cube(bool initialState = false)
            {
                isActive = initialState;
                shouldChange = false;
            }
            
            public void SetShouldChange()
            {
                shouldChange = true;
            }

            public void ChangeState()
            {
                if(shouldChange)
                {
                    isActive = !isActive;
                    shouldChange = false;
                }
            }
        }
    }
}
