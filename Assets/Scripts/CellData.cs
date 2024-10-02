using UnityEngine;

public class CellData
{
    public Vector2 position;
    public bool occupied;
    public TowerData TowerData;

    public CellData(Vector2 position, bool occupied)
    {
        this.position = position;   
        this.occupied = occupied;
    }
}
