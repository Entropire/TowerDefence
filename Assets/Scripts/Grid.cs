using System;
using DefaultNamespace;
using Unity.Mathematics;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static event Action<Vector2Int> OnCellClick;

    [SerializeField] private EnemyPath enemyPath;
    [SerializeField] private Vector2Int cellSize;
    
    private Vector2Int gridSize;
    private CellData[][] grid;
    private float cellSpacing;

    private void Start()
    {
        float cameraHeight = 2f * Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;
        
        int gridHeight = (int)math.ceil(cameraHeight / cellSize.y);
        int gridWidth = (int)math.ceil(cameraWidth / cellSize.x);
        gridSize = new Vector2Int(gridWidth, gridHeight);
        
        GenerateGrid();
        GenerateEnemyPath();
        cellSpacing = Vector2.Distance(grid[0][0].position, grid[0][1].position);
    }

    private void GenerateGrid()
    {
        grid = new CellData[gridSize.y][];
        
        for (int y = 0; y < gridSize.y; y++)
        {
            grid[y] = new CellData[gridSize.x];

            for (int x = 0; x < gridSize.x; x++)
            {
                Vector2 cellPos = new Vector2(transform.position.x + x + .5f, transform.position.y + y + .5f);
                grid[y][x] = new CellData(cellPos, false);
            }
        }
    }

    private void GenerateEnemyPath()
    {
        foreach (var pathTile in enemyPath.path)
        {
            CellData cell = grid[pathTile.y][pathTile.x];
            cell.occupied = true;
        }
    }

    public CellData GetClosestCell(Vector2 position)
    {
        CellData closestCellData = null;
        float closestDistance = float.MaxValue;

        for (int y = 0; y < gridSize.y; y++)
        {
            for (int x = 0; x < gridSize.x; x++)
            {
                float distance = Vector2.Distance(grid[y][x].position, position);
            
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestCellData = grid[y][x];
                }

                if (closestDistance <= cellSpacing / 2)
                {
                    return closestCellData;
                }
            }
        }
        return closestCellData;
    }
}