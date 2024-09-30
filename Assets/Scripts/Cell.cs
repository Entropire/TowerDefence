using UnityEngine;

public class Cell
{
    public Vector2 position;
    public bool occupied;
    public GameObject child;

    public Cell(Vector2 position, bool occupied)
    {
        this.position = position;
        this.occupied = occupied;
    }
}
