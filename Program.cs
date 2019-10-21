using System;
using System.Diagnostics;
using System.Linq;
using Combinatorics.Collections;

namespace DraftKings
{
    public class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1. Football");
                Console.WriteLine("2. Basketball");
                Console.WriteLine("3. Baseball");
                Console.WriteLine();
                Console.Write("Sport: ");
                var sport = Console.ReadLine();

                
                if (sport == "0")
                {
                    Console.Write("Number of players: ");
                    var numberOfPlayers = Console.ReadLine();
                    var list = Enumerable.Range(1, Convert.ToInt32(numberOfPlayers)).ToList();
                    var stopwatch = new Stopwatch();
                    //stopwatch.Start();
                    //var combinations = MathFun.Combs(list, 9).ToList();
                    //stopwatch.Stop();
                    //Console.WriteLine("This took " + stopwatch.ElapsedMilliseconds + "ms");
                    //Console.WriteLine("There are this many combinations: " + combinations.Count);
                    //stopwatch.Reset();
                    stopwatch.Start();
                    var combinations2 = new Combinations<int>(list, 9, GenerateOption.WithoutRepetition);
                    stopwatch.Stop();
                    Console.WriteLine("This took " + stopwatch.ElapsedMilliseconds + "ms");
                    Console.WriteLine("There are this many combinations: " + combinations2.Count);
                    stopwatch.Reset();
                    stopwatch.Start();
                    foreach (var i in combinations2)
                    {
                        //Console.WriteLine(string.Join(",",i));
                    }
                    stopwatch.Stop();
                    Console.WriteLine("This took " + stopwatch.ElapsedMilliseconds + "ms");
                    Console.WriteLine();

                }

                if (sport == "1")
                {
                    Console.Write("DK GroupID: ");
                    var groupId = Console.ReadLine();
                    Console.Write("Week #: ");
                    var week = Console.ReadLine();

                    Football.FootballMain(week, groupId);
                }

                if (sport == "2")
                {
                    Console.WriteLine("Basketball is not implemented yet.");
                }

                if (sport == "3")
                {
                    Console.WriteLine("Baseball is not implemented yet.");
                }
            }

        }
    }
}