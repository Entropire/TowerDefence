using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    public Sprite path;

    public Vector2Int size;

    private GameObject[][] _grid;
    public List<Vector2Int> enemyPath;

    void Start()
    {
        enemyPath = new List<Vector2Int>();

        for (int i = 0; i < size.x; i++)
        {
            enemyPath.Add(new Vector2Int(i, 5));
        }

        LoadGrid(); 
        LoadEnemyPath(); 
    }

    private void LoadGrid()
    {
        _grid = new GameObject[size.y][]; 

        for (int y = 0; y < size.y; y++)
        {
            _grid[y] = new GameObject[size.x];
            for (int x = 0; x < size.x; x++)
            {
                float posX = x + transform.position.x + 0.5f;
                float posY = y + transform.position.y + 0.5f;

                _grid[y][x] = Instantiate(cellPrefab, new Vector3(posX, posY, 0), Quaternion.identity, transform);
            }
        }
    }

    private void LoadEnemyPath()
    {
        foreach (var cell in enemyPath.Select(pos => _grid[pos.y][pos.x]))
        {
            cell.GetComponent<SpriteRenderer>().sprite = path;
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
                Vector3 cellPos = _grid[y][x].transform.position;
                float distance = Vector3.Distance(cellPos, position); 

                if (distance < minDistance) 
                {
                    minDistance = distance;
                    closestPos = cellPos;
                    cell = _grid[y][x].GetComponent<Cell>(); 
                }
            }
        }
        
        return closestPos; 
    }
}