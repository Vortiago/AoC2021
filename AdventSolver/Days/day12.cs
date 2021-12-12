namespace Days;

public class Day12 : IDay
{
    private Node StartNode { get; set; } = new Node();

    public Day12(string s)
    {
        this.ParseInput(s);
    }

    public void ParseInput(string s)
    {
        var nodes = new List<Node>();

        foreach (var row in s.Split(Environment.NewLine))
        {
            var leftNodeDesignation = row.Split('-')[0].Trim();
            var rightNodeDesignation = row.Split('-')[1].Trim();
            var leftNode = nodes.FirstOrDefault(node => node.Designation == leftNodeDesignation)
                ?? this.AddNodeAndReturn(new Node { Designation = leftNodeDesignation }, ref nodes);
            var rightNode = nodes.FirstOrDefault(node => node.Designation == rightNodeDesignation)
                ?? this.AddNodeAndReturn(new Node { Designation = rightNodeDesignation }, ref nodes);
            leftNode.AddHop(rightNode);
            rightNode.AddHop(leftNode);
        }

        this.StartNode = nodes.First(x => x.Designation.Equals("start"));
    }

    private Node AddNodeAndReturn(Node node, ref List<Node> list)
    {
        list.Add(node);
        return node;
    }

    public long Part1()
    {
        var currentPosition = new List<Node>();
        var paths = new List<List<Node>>();
        this.StartNode.VisitNext(currentPosition.ToList(), ref paths);
        return paths.Count();
    }

    public long Part2()
    {
        var currentPosition = new List<Node>();
        var paths = new List<List<Node>>();
        this.StartNode.VisitPart2Next(currentPosition.ToList(), ref paths);
        return paths.Count();
    }

    public class Node
    {
        private List<Node> NextHops = new List<Node>();
        public bool BigCave
        {
            get
            {
                if (string.IsNullOrEmpty(this.Designation)) return false;
                return char.IsUpper(Designation.First()) ? true : false;
            }
        }
        public string Designation { get; set; } = string.Empty;

        public void AddHop(Node nextHop)
        {
            this.NextHops.Add(nextHop);
        }

        public override string ToString()
        {
            return $"{this.Designation} -> {string.Join(',', this.NextHops.Select(x => x.Designation))}";
        }

        internal void VisitNext(List<Node> currentPosition, ref List<List<Node>> paths)
        {
            currentPosition.Add(this);
            if (this.Designation == "end") paths.Add(currentPosition.ToList());
            foreach (var node in this.NextHops.Except(currentPosition.Where(x => !x.BigCave)).ToList())
            {
                node.VisitNext(currentPosition.ToList(), ref paths);
            }
        }

        internal void VisitPart2Next(List<Node> currentPosition, ref List<List<Node>> paths)
        {
            currentPosition.Add(this);
            if (this.Designation == "end")
            {
                paths.Add(currentPosition.ToList());
                return;
            }
            var nodesToVisit = this.NextHops.Except(currentPosition.Where(x => !x.BigCave)).ToList();

            var possibleNodes = this.NextHops.Where(x => !x.BigCave && x.Designation != "start" && x.Designation != "end").Intersect(currentPosition);
            var groups = currentPosition.Where(x => !x.BigCave).GroupBy(x => x.Designation);
            if (!groups.Any(x => x.Count() == 2))
            {
                nodesToVisit.AddRange(possibleNodes);
            }

            foreach (var node in nodesToVisit)
            {
                node.VisitPart2Next(currentPosition.ToList(), ref paths);
            }

        }
    }
}
