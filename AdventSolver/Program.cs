using System.Diagnostics;
using Days;

var days = new List<IDay>
{
    new Day1(LoadInputFile("day1.txt")),
    new Day2(LoadInputFile("day2.txt")),
    new Day3(LoadInputFile("day3.txt")),
    new Day4(LoadInputFile("day4.txt")),
    new Day5(LoadInputFile("day5.txt")),
    new Day6(LoadInputFile("day6.txt")),
    new Day7(LoadInputFile("day7.txt")),
    new Day9(LoadInputFile("day9.txt")),
    new Day11(LoadInputFile("day11.txt")),
    new Day12(LoadInputFile("day12.txt")),
    new Day15(LoadInputFile("day15.txt")),
};

Parallel.ForEach(days, (day) =>
{
    Stopwatch stopWatch = new Stopwatch();
    stopWatch.Start();
    var part1 = day.Part1();
    stopWatch.Stop();
    Console.WriteLine($"{day.GetType().Name}.Part1: {stopWatch.Elapsed.ToString()} {part1}");
    stopWatch.Restart();
    var part2 = day.Part2();
    stopWatch.Stop();
    Console.WriteLine($"{day.GetType().Name}.Part2: {stopWatch.Elapsed.ToString()} {part2}");
   
});

string LoadInputFile(string filename)
{
    using (var streamReader = new StreamReader($"Inputs/{filename}"))
    {
        return streamReader.ReadToEnd();
    }
}