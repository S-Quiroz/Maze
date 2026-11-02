
namespace Backend;

internal class SearchResult
{
    public bool Found { get; }   // propiedad de solución encontrada

    public IReadOnlyList<Position> Path { get; }   // propiedad de la ruta encontrada

    public int CellsExplored { get; }   // propiedad de celdas exploradas
                                        
    public int Steps => Found ? Path.Count - 1 : -1; // contador 

    public SearchResult(bool found, IReadOnlyList<Position> path, int cellsExplored)
    {
        Found = found;
        Path = path;
        CellsExplored = cellsExplored;
    }

    public static SearchResult Success (IReadOnlyList<Position> path, int cellsExplored) => new SearchResult(true, path, cellsExplored);
    
    public static SearchResult NoPath(int cellsExplored) => new (false, Array.Empty<Position>(), cellsExplored);

}




