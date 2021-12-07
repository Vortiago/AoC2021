namespace Days;

public class Day7 : IDay
{
    List<int> numbers = new List<int>();

    public Day7(string s)
    {
        this.numbers.AddRange(ParseInput(s));
    }

    public IEnumerable<int> ParseInput(string s)
    {
        return s.Trim().Split(',').Select(num => Convert.ToInt32(num));
    }

    public long Part1()
    {
        var cost = int.MaxValue;
        
        for (var i = numbers.Min(); i < numbers.Max(); i++)
        {
            var tempCost = numbers.Select(x => Math.Abs(x - i)).Sum();
            if (tempCost < cost)
            {
                cost = tempCost;
            }
        }

        return cost;
    }

    public long Part2()
    {
        var cost = int.MaxValue;
        
        for (var i = numbers.Min(); i < numbers.Max(); i++)
        {

            var tempCost = numbers.Select(x => Enumerable.Range(0, Math.Abs(x - i)).Sum() + Math.Abs(x - i)).Sum();
            if (tempCost < cost)
            {
                cost = tempCost;
            }
        }

        return cost;
    }
}