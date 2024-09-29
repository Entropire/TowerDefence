using UnityEngine;

public class Grid
{
    private Vector2 position;
    
    private Vector2Int gridSize;
    private Vector2Int cellSize;
    private Cell[][] grid;
    private float cellSpacing;
    
    public Grid(Vector2 position, Vector2Int gridSize, Vector2Int cellSize)
    {
        this.position = position;
        this.gridSize = gridSize;
        this.cellSize = cellSize;
        
        GenerateGrid();
        cellSpacing = Vector2.Distance(grid[0][0].position, grid[0][1].position);
    }
    
    private void GenerateGrid()
    {
        grid = new Cell[gridSize.y][];
        
        for (int y = 0; y < gridSize.y; y++)
        {
            grid[y] = new Cell[gridSize.x];

            for (int x = 0; x < gridSize.x; x++)
            {
                Vector2 cellPos = new Vector2(x - position.x, y - position.y);
                grid[y][x] = new Cell(cellPos, false);
            }
        }
    }

    public Cell GetClosestCell(Vector2Int position)
    {
        Cell closestCell = null;
        float closestDistance = float.MaxValue;

        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                float distance = Vector2.Distance(grid[y][x].position, position);
            
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestCell = grid[y][x];
                }

                if (closestDistance <= cellSpacing)
                {
                    return closestCell;
                }
            }
        }
        
        return closestCell;
    }
}
