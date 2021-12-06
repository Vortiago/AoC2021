using System.Collections.Concurrent;

namespace Days;

public class Day6 : IDay
{
    private List<Fish> Ocean = new List<Fish>();

    public Day6(string input)
    {
        this.Ocean.AddRange(this.ParseInput(input));
    }

    public IEnumerable<Fish> ParseInput(string s)
    {
        return s.Trim().Split(',').Select(x => new Fish(Convert.ToInt64(x)));
    }

    /* public long Part1()
    {
        foreach (var num in Enumerable.Range(0, 80))
        {
            var tempOcean = new List<Fish>(this.Ocean);
            foreach (var fish in tempOcean)
            {
                var newFish = fish.AdvanceTime();
                if (newFish != null) this.Ocean.Add(newFish);
            }
        }

        return this.Ocean.Count();
    } */

    public long Part1()
    {
        return OceanLife(80);
    }

    public long Part2()
    {
        return OceanLife(256);
    }

    public long OceanLife(int days)
    {
        var otherOcean = new long[9];
        this.Ocean.ForEach(fish => otherOcean[fish.SpawnTimer] += 1);
        foreach (var i in Enumerable.Range(0, days))
        {
            var nextIteration = new long[9];
            for (var ii = 0; ii < otherOcean.Length; ii++)
            {
                if (ii == 0)
                {
                    nextIteration[6] = otherOcean[ii];
                    nextIteration[8] = otherOcean[ii];
                }
                else
                {
                    nextIteration[ii - 1] += otherOcean[ii];
                }
            }
            otherOcean = nextIteration;
        }
        
        return otherOcean.Sum();
    }

    public class Fish
    {
        public Int64 SpawnTimer { get; private set; }

        public Fish(Int64 SpawnTimer = 8)
        {
            this.SpawnTimer = SpawnTimer;
        }

        public Fish? AdvanceTime()
        {
            if (this.SpawnTimer == 0)
            {
                this.SpawnTimer = 6;
                return new Fish();
            }
            else
            {
                this.SpawnTimer--;
                return null;
            }
        }
    }
}