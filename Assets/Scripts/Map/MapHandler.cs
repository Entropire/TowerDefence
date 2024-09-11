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
            EnemyPath.Add(new Vector2Int(i, 5));
        }

        LoadGrid(); 
        LoadEnemyPath(EnemyPath); 
    }

    private void LoadGrid()
    {
        grid = new GameObject[size.y][]; 

        for (int y = 0; y < size.y; y++)
        {
            grid[y] = new GameObject[size.x];
            for (int x = 0; x < size.x; x++)
            {
                float posX = x + transform.position.x + 0.5f;
                float posY = y + transform.position.y + 0.5f;

                grid[y][x] = Instantiate(cellPrefab, new Vector3(posX, posY, 0), Quaternion.identity, transform);
            }
        }
    }

    private void LoadEnemyPath(List<Vector2Int> path)
    {
        foreach (Vector2Int pos in path)
        {
            GameObject cell = grid[pos.y][pos.x];

            cell.GetComponent<SpriteRenderer>().sprite = Path;
            cell.GetComponent<Cell>().occupied = true;
        }
    }

    public Vector3 GetClosestCell(Vector3 position, out Cell cell)
    {
        cell = null;
        Vector3 closestPos = Vector3.zero;
        float minDistance = Mathf.Infinity; 

        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                Vector3 cellPos = grid[y][x].transform.position;
                float distance = Vector3.Distance(cellPos, position); 

                if (distance < minDistance) 
                {
                    minDistance = distance;
                    closestPos = cellPos;
                    cell = grid[y][x].GetComponent<Cell>(); 
                }
            }
        }

        return closestPos; 
    }
}