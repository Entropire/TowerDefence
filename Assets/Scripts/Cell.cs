using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Vector2 position;
    public bool occupied;

    public Cell(Vector2 position, bool occupied)
    {
        this.position = position;
        this.occupied = occupied;
    }
}
