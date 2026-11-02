
namespace Backend;

internal class SearchResult
{
    public bool Found { get; }   // propiedad de solución encontrada

    public IReadOnlyList<Position> Path { get; }   // propiedad de la ruta encontrada

    public int CellsExplored { get; }   // propiedad de celdas exploradas   
}
