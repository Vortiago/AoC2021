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
};

Parallel.ForEach(days, (day) => {
    Console.WriteLine($"{day.GetType().Name}.Part1: {day.Part1()}");
    Console.WriteLine($"{day.GetType().Name}.Part2: {day.Part2()}");
});

string LoadInputFile(string filename) {
    using (var streamReader = new StreamReader($"Inputs/{filename}")) {
        return streamReader.ReadToEnd();
    }
}