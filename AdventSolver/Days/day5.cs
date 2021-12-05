namespace Days;

public class Day5 : IDay
{
    private Map map = new Map();

    public Day5(string input) {
        if (!string.IsNullOrEmpty(input)) this.map = ParseInput(input);
    }

    public Map ParseInput(string s) {
        var map = new Map();
        foreach(var line in s.Trim().Split(Environment.NewLine)) {
            var parts = line.Trim().Split("->").ToList();
            var start = parts[0].Split(',').Select(x => Convert.ToInt64(x.Trim())).ToList();
            var stop = parts[1].Split(',').Select(x => Convert.ToInt64(x.Trim())).ToList();
            map.Lines.Add(new Map.Line{
                Start = (start[0], start[1]),
                Stop = (stop[0], stop[1])
            });
        }

        return map;
    }

    public long Part1()
    {
        return map.MapStraightValues().SelectMany(x => x).Where(x => x > 1).Count();
    }

    public long Part2()
    {
        return map.MapAllValues().SelectMany(x => x).Where(x => x > 1).Count();
    }

    public class Map {
        public List<Line> Lines {get; init;} = new List<Line>();
        public List<List<Int64>> MapStraightValues() {
            var mapValues = new List<List<Int64>>();
            var size = this.Size();
            for(var i = 0; i <= size.Y; i++) {
                var line = new List<Int64>();
                for (var ii = 0; ii <= size.X; ii++) {
                    line.Add(this.Lines.Where(x => x.StraightLine && x.Intersects(ii, i)).Count());
                }
                mapValues.Add(line);
            }

            return mapValues;
        }

        public List<List<Int64>> MapAllValues() {
            var mapValues = new List<List<Int64>>();
            var size = this.Size();
            for(var i = 0; i <= size.Y; i++) {
                var line = new List<Int64>();
                for (var ii = 0; ii <= size.X; ii++) {
                    line.Add(this.Lines.Where(x => x.Intersects(ii, i)).Count());
                }
                mapValues.Add(line);
            }

            return mapValues;
        }

        public (Int64 X, Int64 Y) Size(){
            Int64 x = 0, y = 0;
            this.Lines.ForEach(line => {
                if (line.Start.X > x) x = line.Start.X;
                if (line.Start.Y > y) y = line.Start.Y;
                if (line.Stop.X > x) x = line.Stop.X;
                if (line.Stop.Y > y) y = line.Stop.Y;
            });

            return (x, y);
        }

        public string ToString(bool straightValues = true) {
            var values = straightValues ? this.MapStraightValues() : this.MapAllValues();
            var s = String.Join(Environment.NewLine, values.Select(line => String.Join(null, line).Replace('0', '.')));

            return s;
        }

        public class Line
        {
            public (Int64 X, Int64 Y) Start {get; set;}
            public (Int64 X, Int64 Y) Stop {get; set;}
            
            public bool StraightLine => Start.X == Stop.X || Start.Y == Stop.Y;

            public bool Intersects(Int64 a, Int64 b) {

                var AB = Math.Sqrt((Stop.X-Start.X)*(Stop.X-Start.X)+(Stop.Y-Start.Y)*(Stop.Y-Start.Y));
                var AP = Math.Sqrt((a-Start.X)*(a-Start.X)+(b-Start.Y)*(b-Start.Y));
                var PB = Math.Sqrt((Stop.X-a)*(Stop.X-a)+(Stop.Y-b)*(Stop.Y-b));
                var result = AB - (AP + PB);
                if((result >= 0 && result < 0.00001) || result <= 0 && result > -0.00001) return true;
                return false;
            }

            public override string ToString() {
                return $"{Start} => {Stop}";
            }
        }
    }
}
