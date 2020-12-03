using System;
using System.IO;
using System.Linq;

namespace AoC_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var numOfLines = lines.Count();
            var lineLength = lines[0].Length; // assume all the same length
            var map2d = new char[numOfLines, lineLength];

            // Populate 2d array
            for (var i = 0; i < numOfLines; i++)
            {
                var thislinearray = lines[i].ToCharArray();

                for (var j = 0; j < lineLength; j++)
                {
                    map2d[i, j] = thislinearray[j]; 
                }
            }

            var excercise1 = CountTrees(map2d, numOfLines, lineLength, 1, 1);
            var excercise2 = CountTrees(map2d, numOfLines, lineLength, 1, 3);
            var excercise3 = CountTrees(map2d, numOfLines, lineLength, 1, 5);
            var excercise4 = CountTrees(map2d, numOfLines, lineLength, 1, 7);
            var excercise5 = CountTrees(map2d, numOfLines, lineLength, 2, 1);
        }

        private static int CountTrees(char[,] map, int totalLines, int lineLength, int xIncrement, int yIncrement)
        {
            var treeCounter = 0;
            var y = 0;
            for (var x = 0; x < totalLines; x+=xIncrement)
            {
                if (map[x, y] == '#')
                {
                    treeCounter++;
                }

                y += yIncrement;

                if (y >= lineLength)
                {
                    // reset y to start of line because whole pattern repeats
                    y -= lineLength;
                }
            }

            Console.WriteLine(treeCounter);
            return treeCounter;
        }
    }
}