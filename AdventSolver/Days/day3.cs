namespace Days;

public class Day3 : IDay
{
    private List<string> input;

    public Day3(string input)
    {
        this.input = input.Split(Environment.NewLine).Select(row => row.Trim()).ToList();
    }

    public IEnumerable<int> FindMostCommon()
    {
        var list = new List<int>();
        list = input.First().Select(x => 0).ToList();

        foreach (var row in input)
        {
            for (var i = 0; i < row.Length; i++)
            {
                if (row[i] == '1')
                {
                    list[i] += 1;
                }
                else
                {
                    list[i] -= 1;
                }
            };
        }

        list = list.Select(x => x > 0 ? 1 : 0).ToList();

        return list;
    }

    public long Part1()
    {
        var mostCommon = FindMostCommon();
        var mostCommonString = string.Join(null, mostCommon.Select(num => num.ToString()));
        var leastCommon = mostCommon.Select(x => x == 1 ? 0 : 1);
        var leastCommonString = string.Join(null, leastCommon.Select(num => num.ToString()));
        return Convert.ToInt64(mostCommonString, 2) * Convert.ToInt64(leastCommonString, 2);
    }

    public long Part2()
    {
        throw new NotImplementedException();
    }
}
