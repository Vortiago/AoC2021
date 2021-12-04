namespace Days;

public class Day4 : IDay
{
    private string input;
    private IEnumerable<Int64> draws;
    private List<Board> boards;

    public Day4(string input) {
        this.input = input;
        this.draws = ParseDraws(input.Split(Environment.NewLine)[0]);
        this.boards = ParseBoards(
            string.Join(Environment.NewLine, 
                input.Split(Environment.NewLine).Skip(2)));
    }

    public IEnumerable<Int64> ParseDraws(string s) {
        return s.Trim().Split(',').Select(x => Convert.ToInt64(x));
    }

    public List<Board> ParseBoards(string s) {
        var list = new List<Board>();
        foreach(var board in s.Split(Environment.NewLine + Environment.NewLine)) {
            list.Add(ParseSingleBoard(board));
        }

        return list;
    }

    public Board ParseSingleBoard(string s) {
        var board = new Board();
        foreach (var row in s.Split(Environment.NewLine)) {
            board.Rows.Add(row.Trim().Split(' ').Where(cell => !string.IsNullOrEmpty(cell)).Select(cell => new Board.Cell{
                Value = Convert.ToInt64(cell)
            }).ToList());
        }

        return board;
    }

    public List<(long, long, Board)> FindWinnerBoard(List<Board> boards) {
        var winners = new List<(long, long, Board)>();
        foreach(var draw in draws) {
            boards.ForEach(board => {
                board.MarkNumber(draw);
            });
            var bingoBoard = boards.Where(board => board.CheckBingo());
            if (bingoBoard.Any()) {
                foreach(var bingo in bingoBoard) {
                    winners.Add((draw, bingo.Sum(), bingo));
                }
                boards = boards.Except(bingoBoard).ToList();
            };
        }
        return winners;
    }

    public long Part1()
    {
        var winnerBoard = FindWinnerBoard(this.boards).First();
        return winnerBoard.Item1 * winnerBoard.Item2;
    }

    public long Part2()
    {
        this.boards = ParseBoards(
            string.Join(Environment.NewLine, 
                input.Split(Environment.NewLine).Skip(2)));
        var lastWinner = FindWinnerBoard(this.boards).Last();
        return lastWinner.Item1 * lastWinner.Item2;
    }

    public class Board {
        public List<List<Cell>> Rows {get;set;} = new List<List<Cell>>();

        public class Cell {
            public bool Marked {get;set;}
            public Int64 Value {get;set;}

            public void CheckValue(Int64 calledNumber) {
                if (this.Value == calledNumber) {
                    this.Marked = true;
                }
            }
        }

        public void MarkNumber(Int64 calledNumber) {
            this.Rows.ForEach(row => row.ForEach(cell => cell.CheckValue(calledNumber)));
        }

        public bool CheckBingo() {
            var rowBingo = this.Rows.Any(row => row.All(cell => cell.Marked));
            var columnBingo = this.Rows.Aggregate((result, next) => {
                var numItems = next.Count();
                var cells = new List<Cell>();
                for(var i = 0; i<numItems; i++) {
                    var cell = new Cell();
                    if (result[i].Marked && next[i].Marked) cell.Marked = true; 
                    cells.Add(cell);
                }
                return cells;
            }).Any(x => x.Marked);

            return rowBingo || columnBingo;
        }

        public Int64 Sum() {
            return this.Rows.SelectMany(row => row.Where(cell => !cell.Marked).Select(cell => cell.Value)).Sum();
        }
    }
}