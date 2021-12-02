namespace Days;

public class Day1 : IDay
{
    private string input;

    public Day1(string input) => this.input = input;

    public Int64 Part1()
    {
        var count = 0;
        var split = input.Split(Environment.NewLine).Select(item => Convert.ToInt64(item)).ToList();
        for (var i = 1; i < split.Count(); i++)
        {
            if (split[i] > split[i - 1])
            {
                count++;
            }
        }

        return count;
    }

    public Int64 Part2()
    {
        var count = 0;
        var split = input.Split(Environment.NewLine).Select(item => Convert.ToInt64(item)).ToList();
        for (var i = 0; i < split.Count() - 3; i++)
        {
            var first = split[i] + split[i + 1] + split[i + 2];
            var second = split[i + 1] + split[i + 2] + split[i + 3];

            if (second > first)
            {
                count++;
            }
        }
        return count;
    }
}
