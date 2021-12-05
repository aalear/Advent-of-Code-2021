var input = File.ReadAllLines("input.txt").ToList();

// Input parser
var numberSource = input[0].Split(",").Select(n => int.Parse(n)).ToArray();
var boards = new List<Board>();
for (var i = 1; i < input.Count; i++)
{
    var line = input[i];
    if (string.IsNullOrEmpty(line))
    {
        boards.Add(new Board());
        continue;
    }

    var board = boards.Last();

    var row = input[i].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(n => new Board.Cell { Value = int.Parse(n) });
    board.AddCells(row);
}

// Part 1
foreach (var n in numberSource)
{
    foreach (var board in boards)
    {
        board.MarkNumber(n);

        if (board.HasWon)
        {
            Console.WriteLine(board.GetScore());
            break;
        }
    }

    if (boards.Any(b => b.HasWon))
    {
        break;
    }
}

// Reset
foreach (var board in boards)
{
    board.Reset();
}

// Part 2
Board? lastWinningBoard = null;
foreach (var n in numberSource)
{
    foreach (var board in boards.Where(b => !b.HasWon))
    {
        board.MarkNumber(n);

        if (board.HasWon)
        {
            lastWinningBoard = board;
        }
    }
}
Console.WriteLine(lastWinningBoard?.GetScore());

class Board
{
    public class Cell
    {
        public int Value { get; set; }
        public bool Found { get; set; } = false;
    }

    private int _winningNumber = 0;
    private readonly List<Cell> _cells = new();

    public bool HasWon { get; set; }

    public int GetScore() => _cells.Where(s => !s.Found).Sum(s => s.Value) * _winningNumber;

    public void AddCells(IEnumerable<Cell> cells) => _cells.AddRange(cells);

    public void MarkNumber(int number)
    {
        foreach (var cell in _cells.Where(c => c.Value == number))
        {
            cell.Found = true;
        }

        // Check all rows for winners
        for (var r = 0; r < 25; r += 5)
        {
            var row = _cells.Skip(r).Take(5);
            if (row.All(s => s.Found))
            {
                _winningNumber = number;
                HasWon = true;
            }
        }

        // Check all columns for winners
        for (var c = 0; c < 5; c++)
        {
            var column = _cells.Where((_, idx) => idx % 5 == c);
            if (column.All(s => s.Found))
            {
                _winningNumber = number;
                HasWon = true;
            }
        }
    }

    public void Reset()
    {
        HasWon = false;
        foreach (var cell in _cells) 
        {
            cell.Found = false;
        }
    }
}