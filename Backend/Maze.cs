
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Backend;

public class Maze
{
    public const char Wall = '#';

    public const char Open = '.';

    public const char Start = 'S';

    public const char Exit = 'E';

    private readonly char[,] _cells;

    public int Rows { get; }

    public int Columns { get; }

    public Position StartPosition { get; }

    public Position ExitPosition { get; }

    private static readonly (int RowOffset, int ColumnOffset)[] Directions =
    {
            (-1, 0), // Up
            (1, 0),  // Down
            (0, -1), // Left
            (0, 1)   // Right
        };

    public Maze(string[] lines)
    {
        if (lines is null || lines.Length == 0)
        {
            throw new ArgumentException("Maze cannot be empty.");
        }

        Rows = lines.Length;
        Columns = lines[0].Length;
        _cells = new char[Rows, Columns];

        Position? start = null; // objets
        Position? exit = null; // objets  

        for (int r = 0; r < Rows; r++)
        {
            if (lines[r].Length != Columns)
            {
                throw new ArgumentException($"Row {r} does not have {Columns} columns.");
            }

            for (int c = 0; c < Columns; c++)
            {
                char ch = lines[r][c];
                _cells[r, c] = ch;

                if (ch == Start) start = new Position(r, c);
                if (ch == Exit) exit = new Position(r, c);
            }
        }
        StartPosition = start ?? throw new ArgumentException("Missing start cell 'S'.");
        ExitPosition = exit ?? throw new ArgumentException("Missing exit cell 'E'.");

    }
    public bool IsInside(Position p) => p.Row >= 0 && p.Row < Rows && p.Column >= 0 && p.Column < Columns;

    public bool IsWalkable(Position p) => IsInside(p) && _cells[p.Row, p.Column] != Wall;

    public char CellAt(Position p) => _cells[p.Row, p.Column];

    public IEnumerable<Position> GetNeighbors(Position p)  // posiciones a entender ( vecinos)  
    {
        foreach (var (rowOffset, columnOffset) in Directions)
        {
            var neighbour = p.Move(rowOffset, columnOffset);
            if (IsWalkable(neighbour))
            {
                yield return neighbour;  // palabra reservada 

            }
        }
    }


    public string Render(IReadOnlyCollection<Position> path = null)
    {

        var onPath = path is null ? new HashSet<Position>() : new HashSet<Position>(path);
        var sb = new System.Text.StringBuilder();

        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Columns; c++)
            {
                var pos = new Position(r, c);
                char ch = _cells[r, c];

                bool isOnPath = onPath.Contains(pos) && ch == Open;
                sb.Append(isOnPath ? '*' : ch);
                sb.Append(' '); 

            }
            sb.AppendLine();    
        }
        return sb.ToString();   

    }

}   

