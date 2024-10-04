using UnityEngine;

public class CellData
{
    public Vector2 position;
    public bool occupied;
    public int textureID;
    public TowerData TowerData;

    public CellData(Vector2 position, bool occupied, int textureID)
    {
        this.position = position;   
        this.occupied = occupied;
    }
}
