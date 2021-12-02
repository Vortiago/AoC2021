namespace Days;

public class Day2 : IDay
{
    private IEnumerable<MoveCommand> moveCommands;
   


    public Day2(string input)
    {
        this.moveCommands = input.Split(Environment.NewLine).Select(x => ParseMoveCommand(x));
    }

    public MoveCommand ParseMoveCommand(string s)
    {
        return new MoveCommand
        {
            Direction = s.Split(' ')[0],
            Distance = Convert.ToInt64(s.Split(' ')[1])
        };
    }

    public long Part1()
    {
        Int64 HorizontalPlane = 0;
        Int64 Depth = 0;

        foreach (var command in moveCommands)
        {
            switch (command.Direction)
            {
                case "forward":
                    HorizontalPlane += command.Distance;
                    break;
                case "down":
                    Depth += command.Distance;
                    break;
                case "up":
                    Depth -= command.Distance;
                    break;
            }
        }

        return HorizontalPlane * Depth;
    }

    public long Part2()
    {
        Int64 HorizontalPlane = 0;
        Int64 Depth = 0;
        Int64 Aim = 0;
        
        foreach (var command in moveCommands)
        {
            switch (command.Direction)
            {
                case "forward":
                    HorizontalPlane += command.Distance;
                    Depth += Aim * command.Distance;
                    break;
                case "down":
                    Aim += command.Distance;
                    break;
                case "up":
                    Aim -= command.Distance;
                    break;
            }
        }

        return HorizontalPlane * Depth;
    }

    public class MoveCommand
    {
        public string Direction { get; set; } = String.Empty;
        public Int64 Distance { get; set; }
    }
}