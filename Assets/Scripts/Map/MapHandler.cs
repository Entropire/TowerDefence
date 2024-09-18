using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

public class MapHandler : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    public Sprite pathSprite;

    public Vector2Int size;

    private Cell[][] _grid;
    public List<Path> enemyPath;

    void Start()
    {
        enemyPath = new List<Path>();

        for (int i = 0; i < size.x; i++)
        {
            Path path = new Path();
            path.position = new(i, 5);
            enemyPath.Add(path);
        }

        LoadGrid(); 
        LoadEnemyPath(); 
    }

    private void LoadGrid()
    {
        _grid = new Cell[size.y][]; 

        for (int y = 0; y < size.y; y++)
        {
            _grid[y] = new Cell[size.x];
            for (int x = 0; x < size.x; x++)
            {
                float posX = x + transform.position.x + 0.5f;
                float posY = y + transform.position.y + 0.5f;

                _grid[y][x] = Instantiate(cellPrefab, new Vector3(posX, posY, 0), Quaternion.identity, transform).GetComponent<Cell>();
                _grid[y][x].position = new (x, y);
            }
        }
    }

    private void LoadEnemyPath()
    {
        foreach (Cell cell in enemyPath.Select(path => _grid[(int)path.position.y][(int)path.position.x]))
        {
            GameObject cellObj = cell.gameObject;
            Path path = cellObj.AddComponent<Path>();
            
            path.occupied = true;
            
            int index = enemyPath.IndexOf(path);
            Path nextCell = enemyPath[index + 1];
            path.deraction = cell.position - nextCell.position;
            
            cellObj.GetComponent<SpriteRenderer>().sprite = pathSprite;
            Destroy(cell.GetComponent<Cell>());
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
                Vector3 cellPos = _grid[y][x].position;
                float distance = Vector3.Distance(cellPos, position); 

                if (distance < minDistance) 
                {
                    minDistance = distance;
                    closestPos = cellPos;
                    cell = _grid[y][x]; 
                }
            }
        }
        
        return closestPos; 
    }
}