namespace Days;

public class Day11 : IDay
{
    public Octopus[,] octopuses { get; set; } = new Octopus[0, 0];
    public List<Octopus> flattenOctopuses = new List<Octopus>();

    public Day11(string s)
    {
        this.ParseInput(s);
    }

    public void ParseInput(string s)
    {
        var rows = s.Split(Environment.NewLine);
        var rowLength = rows.Length;
        var colLength = rows.First().Length;
        this.octopuses = new Octopus[rowLength, colLength];
        for (var row = 0; row < rowLength; row++)
        {
            for (var col = 0; col < colLength; col++)
            {
                var octopus = new Octopus
                {
                    Energy = Convert.ToInt64(rows[row][col].ToString()),
                };

                this.octopuses[row, col] = octopus;
                this.flattenOctopuses.Add(octopus);
            }
        }
    }

    public void StepOne()
    {
        foreach (var octopus in this.flattenOctopuses)
        {
            octopus.Energy += 1;
        }
    }

    public void IncreaseOcto(Int64 row, Int64 col)
    {
        try
        {
            this.octopuses[row, col].Energy += 1;
        }
        catch
        {
            // Ignore out of bounds...
        }
    }

    public void Flashy()
    {
        for (var row = 0; row < this.octopuses.GetLength(0); row++)
        {
            for (var col = 0; col < this.octopuses.GetLength(1); col++)
            {
                if (this.octopuses[row, col].Flashed)
                {
                    this.IncreaseOcto(row - 1, col - 1);
                    this.IncreaseOcto(row - 1, col);
                    this.IncreaseOcto(row - 1, col + 1);
                    this.IncreaseOcto(row, col - 1);
                    this.IncreaseOcto(row, col);
                    this.IncreaseOcto(row, col + 1);
                    this.IncreaseOcto(row + 1, col - 1);
                    this.IncreaseOcto(row + 1, col);
                    this.IncreaseOcto(row + 1, col + 1);
                    this.octopuses[row, col].Flashed = false;
                }
            }
        }
    }

    public long Part1()
    {
        for (var step = 0; step < 100; step++)
        {
            this.StepOne();
            while (this.flattenOctopuses.Any(octopus => octopus.Flashed))
            {
                this.Flashy();
            }

            foreach(var octo in this.flattenOctopuses) {
                if (octo.Energy >= 10) {
                    octo.Energy = 0;
                }
            }
        }

        return this.flattenOctopuses.Sum(x => x.Flashes);
    }

    public long Part2()
    {
        for (var step = 1; step < 10000; step++)
        {
            this.StepOne();
            while (this.flattenOctopuses.Any(octopus => octopus.Flashed))
            {
                this.Flashy();
            }

            foreach(var octo in this.flattenOctopuses) {
                if (octo.Energy >= 10) {
                    octo.Energy = 0;
                }
            }

            if (this.flattenOctopuses.All(x => x.Energy == 0)) {
                return 100 + step;
            }
        }

        return 0L;
    }

    public class Octopus
    {
        private Int64 energy = 0;
        public Int64 Energy
        {
            get => this.energy;
            set
            {
                this.energy = value;
                if (value == 10)
                {
                    this.Flashed = true;
                    this.Flashes += 1;
                }
            }
        }

        public Int64 Flashes = 0;
        public bool Flashed { get; set; }

        public override string ToString()
        {
            return Energy.ToString();
        }
    }
}
