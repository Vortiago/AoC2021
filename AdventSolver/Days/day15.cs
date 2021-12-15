namespace Days;

public class Day15 : IDay
{
    public MapPoint[,] Map { get; set; } = new MapPoint[0, 0];
    public MapPoint[,] ExtendedMap { get; set; } = new MapPoint[0, 0];

    public List<MapPoint> flattenedMap { get; set; } = new List<MapPoint>();

    public List<MapPoint> flattenedExtendedMap { get; set; } = new List<MapPoint>();
    public Day15(string map)
    {
        this.ParseInput(map);
    }

    public void ParseInput(string map)
    {
        var rows = map.Split(Environment.NewLine);
        var rowCount = rows.Count();
        var colCount = rows.First().Length;

        this.Map = new MapPoint[rowCount, colCount];
        this.ExtendedMap = new MapPoint[rowCount * 5, colCount * 5];

        for (var row = 0; row < rowCount; row++)
        {
            for (var col = 0; col < colCount; col++)
            {
                var mapPoint = new MapPoint
                {
                    Value = Convert.ToInt64(rows[row][col].ToString()),
                    Position = (row, col),
                };
                this.Map[row, col] = mapPoint;
                this.flattenedMap.Add(mapPoint);
                for (var extRows = 0; extRows < 5; extRows++)
                {
                    for (var extCol = 0; extCol < 5; extCol++)
                    {
                        var val = Convert.ToInt64(rows[row][col].ToString());
                        val += extRows + extCol;
                        if (val > 9) val -= 9;
                        mapPoint = new MapPoint
                        {
                            Value =  val,
                            Position = (row + (extRows * rowCount), col + (extCol * colCount)),
                        };
                        this.ExtendedMap[(row + (extRows * rowCount)), col + (extCol * colCount)] = mapPoint;
                        this.flattenedExtendedMap.Add(mapPoint);
                    }
                }
            }
        }
    }

    public MapPoint? GetMapPoint(int row, int col, bool extended = false)
    {
        MapPoint? point = null;
        try
        {
            if (extended)
            {
                point = this.ExtendedMap[row, col];
            }
            else
            {
                point = this.Map[row, col];
            }
        }
        catch { }

        return point;
    }

    public void ProcessPoint(MapPoint current, MapPoint next)
    {
        if (!next.Visited)
        {
            var cost = current.Cost + next.Value;
            next.Cost = Math.Min(next.Cost, cost);
        }
    }

    public long Part1()
    {
        this.Map[0, 0].Cost = 0;
        while (this.flattenedMap.Any(point => !point.Visited))
        {
            var currentPoint = this.flattenedMap.Where(x => !x.Visited).OrderBy(x => x.Cost).FirstOrDefault();
            if (currentPoint == null) break;
            var left = GetMapPoint(currentPoint.Position.row, currentPoint.Position.col - 1);
            if (left != null) ProcessPoint(currentPoint, left);
            var up = GetMapPoint(currentPoint.Position.row - 1, currentPoint.Position.col);
            if (up != null) ProcessPoint(currentPoint, up);
            var right = GetMapPoint(currentPoint.Position.row, currentPoint.Position.col + 1);
            if (right != null) ProcessPoint(currentPoint, right);
            var down = GetMapPoint(currentPoint.Position.row + 1, currentPoint.Position.col);
            if (down != null) ProcessPoint(currentPoint, down);
            currentPoint.Visited = true;
        }

        return this.Map[this.Map.GetLength(0) - 1, this.Map.GetLength(1) - 1].Cost;
    }

    public long Part2()
    {
        this.ExtendedMap[0, 0].Cost = 0;
        while (this.flattenedExtendedMap.Any())
        {
            var currentPoint = this.flattenedExtendedMap.OrderBy(x => x.Cost).FirstOrDefault();
            if (currentPoint == null) break;
            var left = GetMapPoint(currentPoint.Position.row, currentPoint.Position.col - 1, true);
            if (left != null) ProcessPoint(currentPoint, left);
            var up = GetMapPoint(currentPoint.Position.row - 1, currentPoint.Position.col, true);
            if (up != null) ProcessPoint(currentPoint, up);
            var right = GetMapPoint(currentPoint.Position.row, currentPoint.Position.col + 1, true);
            if (right != null) ProcessPoint(currentPoint, right);
            var down = GetMapPoint(currentPoint.Position.row + 1, currentPoint.Position.col, true);
            if (down != null) ProcessPoint(currentPoint, down);
            currentPoint.Visited = true;
            this.flattenedExtendedMap.Remove(currentPoint);
        }

        return this.ExtendedMap[this.ExtendedMap.GetLength(0) - 1, this.ExtendedMap.GetLength(1) - 1].Cost;
    }

    public class MapPoint
    {
        public Int64 Value { get; set; }
        public (int row, int col) Position { get; set; }

        public bool Visited { get; set; } = false;

        public Int64 Cost { get; set; } = Int64.MaxValue;


        public override string ToString()
        {
            return $"({Visited}|{Position.row},{Position.col})->{Value}|{Cost}";
        }
    }
}
