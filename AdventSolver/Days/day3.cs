namespace Days;

public class Day3 : IDay
{
    private List<string> inputStrings;

    public Day3(string input)
    {
        this.inputStrings = this.ParseInput(input);
    }

    public List<string> ParseInput(string s) {
        return s.Split(Environment.NewLine).Select(row => row.Trim()).ToList();
    }

    public IEnumerable<int> FindMostCommon(IEnumerable<string> input)
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

        list = list.Select(x => x > 0 ? 1 : x < 0 ? -1 : 0).ToList();

        return list;
    }

    public IEnumerable<string> FindSingleMostCommon(IEnumerable<string> input) {
        var workingList = input;
        var length = input.First().Length;
        for(var i = 0; i < length; i++) {
            var mostCommon = FindMostCommon(workingList).ToArray();
            if (mostCommon[i] >= 0) {
                workingList = workingList.Where(x => x[i] == '1').ToList();
            } else {
                workingList = workingList.Where(x => x[i] == '0').ToList();
            }
            if (workingList.Count() == 1) return workingList;
        }

        return workingList;
    }

    public IEnumerable<string> FindSingleLeastCommon(IEnumerable<string> input) {
        var workingList = input;
        var length = input.First().Length;
        for(var i = 0; i < length; i++) {
            var mostCommon = FindMostCommon(workingList).ToArray();
            if (mostCommon[i] >= 0) {
                workingList = workingList.Where(x => x[i] == '0').ToList();
            } else {
                workingList = workingList.Where(x => x[i] == '1').ToList();
            }
            if (workingList.Count() == 1) return workingList;
        }

        return workingList;
    }

    public long Part1()
    {
        var mostCommon = FindMostCommon(this.inputStrings).Select(x => x >= 0 ? 1 : 0);
        var mostCommonString = string.Join(null, mostCommon.Select(num => num.ToString()));
        var leastCommon = mostCommon.Select(x => x == 1 ? 0 : 1);
        var leastCommonString = string.Join(null, leastCommon.Select(num => num.ToString()));
        return Convert.ToInt64(mostCommonString, 2) * Convert.ToInt64(leastCommonString, 2);
    }

    public long Part2()
    {
        return Convert.ToInt64(FindSingleMostCommon(this.inputStrings).First(), 2) * Convert.ToInt64(FindSingleLeastCommon(this.inputStrings).First(), 2);
    }
}
