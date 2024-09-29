using Unity.Mathematics;
using UnityEngine;

public class TowerPlacing : MonoBehaviour
{
    private Grid grid;
    private Vector2Int cellSize;
    void Start()
    {
        float cameraHeight = 2f * Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;
        
        int gridHeight = (int)math.ceil(cameraHeight / cellSize.y);
        int gridWidth = (int)math.ceil(cameraWidth / cellSize.x);
        
        grid = new Grid(transform.position, new Vector2Int(gridHeight, gridWidth), cellSize);        
    }
}
