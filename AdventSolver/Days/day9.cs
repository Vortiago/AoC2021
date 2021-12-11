namespace Days;

public class Day9 : IDay
{
    private List<HeightNumber> Numbers { get; set; } = new List<HeightNumber>();

    private HeightNumber[,] radarArray = new HeightNumber[0,0];

    private Int64[,] array;

    public Day9(string s)
    {
        this.array = ParseInput(s);
    }

    public Int64[,] ParseInput(string s)
    {
        var lines = s.Split(Environment.NewLine);
        var array = new Int64[lines.Count(), lines.First().Length];
        radarArray = new HeightNumber[lines.Count(), lines.First().Length];

        for (var row = 0; row < lines.Length; row++)
        {
            for (var col = 0; col < lines.First().Length; col++)
            {
                array[row, col] = Convert.ToInt64(lines[row][col].ToString());
                var heightNumber = new HeightNumber
                {
                    Value = Convert.ToInt64(lines[row][col].ToString()),
                };

                if (heightNumber.Value < 9)
                {

                    if (row > 0)
                    {
                        if (radarArray[row - 1, col].Value < 9)
                        {
                            heightNumber.Above = radarArray[row - 1, col];
                        }
                    }

                    if (col > 0)
                    {
                        if (radarArray[row, col - 1].Value < 9)
                        {
                            heightNumber.Left = radarArray[row, col - 1];
                        }
                    }
                }
                radarArray[row, col] = heightNumber;
            }
        }
        return array;
    }

    public long Part1()
    {
        for (var row = 0; row < array.GetLength(0); row++)
        {
            for (var col = 0; col < array.GetLength(1); col++)
            {
                var number = new HeightNumber
                {
                    Value = array[row, col],
                };

                if (col > 0)
                {
                    number.Neighbours.Add(array[row, col - 1]);
                }

                if (col < array.GetLength(1) - 1)
                {
                    number.Neighbours.Add(array[row, col + 1]);
                }

                if (row > 0)
                {
                    number.Neighbours.Add(array[row - 1, col]);
                }

                if (row < array.GetLength(0) - 1)
                {
                    number.Neighbours.Add(array[row + 1, col]);
                }

                this.Numbers.Add(number);
            }
        }

        return Numbers.Where(x => x.Lowest).Select(x => 1 + x.Value).Sum();
    }

    public Int64 AggregateBasin(ref HeightNumber number) {
        if (number == null || number.Counted) return 0L;
        var basinSize = 1L;
        number.Counted = true;
        var above = number.Above;
        var left = number.Left;
        var right = number.Right;
        var below = number.Below;
        if (above != null) basinSize += AggregateBasin(ref above);
        if (left != null) basinSize += AggregateBasin(ref left);
        if (right != null) basinSize += AggregateBasin(ref right);
        if (below != null) basinSize += AggregateBasin(ref below);
        return basinSize;
    }

    public long Part2()
    {
        var basinSizes = new List<Int64>();
        for(var row = 0; row < radarArray.GetLength(0); row++) {
            for(var col = 0; col < radarArray.GetLength(1); col ++) {
                if (!radarArray[row, col].Counted) {
                    basinSizes.Add(AggregateBasin(ref radarArray[row, col]));
                }
            }
        }

        return basinSizes.OrderByDescending(x => x).Take(3).Aggregate((a, b) => a*b);
    }

    public class HeightNumber
    {
        private HeightNumber? left;
        private HeightNumber? right;
        private HeightNumber? above;
        private HeightNumber? below;

        public HeightNumber? Left
        {
            get => this.left;
            set
            {
                if (value != null) value.Right = this;
                this.left = value;
            }
        }

        public HeightNumber? Right
        {
            get => this.right;
            set
            {
                this.right = value;
            }
        }

        public HeightNumber? Above
        {
            get => this.above;
            set
            {
                if (value != null) value.Below = this;
                this.above = value;
            }
        }

        public HeightNumber? Below
        {
            get => this.below;
            set
            {
                this.below = value;
            }
        }

        public List<Int64> Neighbours { get; } = new List<Int64>();
        public Int64 Value = 0;
        public bool Counted;
        public bool Lowest => this.Value < this.Neighbours.Min();

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
