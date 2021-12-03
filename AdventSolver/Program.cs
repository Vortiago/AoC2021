using Days;

var days = new List<IDay>
{
    // new Day1(LoadInputFile("day1.txt")),
    // new Day2(LoadInputFile("day2.txt")),
    new Day3(LoadInputFile("day3.txt"))
};

foreach(var day in days) {
    Console.WriteLine($"{day.GetType().Name}.Part1: {day.Part1()}");
    Console.WriteLine($"{day.GetType().Name}.Part2: {day.Part2()}");
}

string LoadInputFile(string filename) {
    using (var streamReader = new StreamReader($"Inputs/{filename}")) {
        return streamReader.ReadToEnd();
    }
}