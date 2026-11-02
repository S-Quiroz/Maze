namespace Backend;

public class BfsPathFinder
{
    private readonly Maze _maze;

    public BfsPathFinder(Maze maze)
    {
        _maze = maze ?? throw new ArgumentNullException(nameof(maze));
    }

    public SearchResult FindShortestPath() // camino más corto  
    {
        var start = _maze.StartPosition;
       
        var exit = _maze.ExitPosition;
      
        var queue = new Queue<Position>(); // cola para almacenar las posiciones a explorar

        var visited = new HashSet<Position>();  // lista sin repetidos 

        var parent = new Dictionary<Position, Position>(); // diccionario para almacenar la posición padre de cada posición visitada

        queue.Enqueue(start); // encola la posición de inicio
        visited.Add(start);

        int cellsExplored = 0; 

        while (queue.Count > 0) // contador de celdas exploradas
        {
            Position current = queue.Dequeue();
            cellsExplored++;   
           
            
            if (current == exit)
              return SearchResult.Success(RebuildPath(parent, start, exit), cellsExplored);
               
        
            foreach (var neighbor in _maze.GetNeighbors(current))
            {
                if (visited.Contains(neighbor))
                    continue;   // si la posición ya fue visitada, se omite
                {
                    visited.Add(neighbor);  
                    parent[neighbor] = current;
                    queue.Enqueue(neighbor);
                }
            }
        }

        return SearchResult.NoPath(visited.Count);
    }

    private static IReadOnlyList<Position> RebuildPath(
        Dictionary<Position, Position> parent,
        Position start,
        Position exit)
    {
        var path = new List<Position>();
        var current = exit;

        path.Add(current);

        while (current != start)
        {
            current = parent[current];
            path.Add(current);
        }

        path.Reverse();
        return path;
    }
}


