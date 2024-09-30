using Unity.Mathematics;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] private Vector2Int cellSize;
    
    private Vector2Int gridSize;
    private Cell[][] grid;
    private float cellSpacing;

    private void Start()
    {
        TowerPlacing.OnTowerPlace += PlaceTower;
        
        float cameraHeight = 2f * Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;
        
        int gridHeight = (int)math.ceil(cameraHeight / cellSize.y);
        int gridWidth = (int)math.ceil(cameraWidth / cellSize.x);
        gridSize = new Vector2Int(gridWidth, gridHeight);
        
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
                Vector2 cellPos = new Vector2(transform.position.x + x + .5f, transform.position.y + y + .5f);
                grid[y][x] = new Cell(cellPos, false);
            }
        }
    }

    private void PlaceTower(GameObject tower)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        Cell cell = GetClosestCell(mousePos);

        if (!cell.occupied)
        {
            cell.occupied = true;
            cell.child = tower;
            tower.transform.position = cell.position;
        }
    }

    private Cell GetClosestCell(Vector2 position)
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

                if (closestDistance <= cellSpacing / 2)
                {
                    return closestCell;
                }
            }
        }
        
        return closestCell;
    }
}
