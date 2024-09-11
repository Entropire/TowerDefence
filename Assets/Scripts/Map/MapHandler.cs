using System.Collections.Generic;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Sprite Path;

    public Vector2Int size;

    private GameObject[][] grid;

    void Start()
    {
        List<Vector2Int> EnemyPath = new List<Vector2Int>();
        for (int i = 0; i < size.x; i++)
        {
            EnemyPath.Add(new(5, i));
        }

        LoadGrid();
        LoadEnemyPath(EnemyPath);
    }

    private void LoadGrid()
    {
        grid = new GameObject[size.y][];

        for(int y = 0; y < size.y; y++)
        {
            grid[y] = new GameObject[size.x];
            for(int x = 0; x < size.x; x++)
            {
                float posX = x + transform.position.x + 0.5f;
                float posY = y + transform.position.y + 0.5f;

                grid[y][x] = Instantiate(cellPrefab, new Vector3(posX, posY, 0), Quaternion.Euler(0,0,0), transform);   
            }
        }
    }

    private void LoadEnemyPath(List<Vector2Int> path)
    {
        foreach (Vector2Int pos in path)
        {
            GameObject cell = grid[pos.x][pos.y];
            cell.GetComponent<SpriteRenderer>().sprite = Path;
            cell.GetComponent<Cell>().occupied = true;
        }
    }
}
