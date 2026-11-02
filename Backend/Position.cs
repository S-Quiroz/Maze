namespace Backend;

    public readonly record struct Position(int Row, int Column)
    {

    public Position Move(int rowOffset, int columnOffset) => new Position(Row + rowOffset, Column + columnOffset);


    public override string ToString() => $"({Row}, {Column})";



    }

