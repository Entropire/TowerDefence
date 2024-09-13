using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private MapHandler mapHandler;
    private Vector2 target;
    private int pathIndex;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mapHandler = GameObject.Find("Map").GetComponent<MapHandler>();

        transform.position = new Vector2(mapHandler.enemyPath[pathIndex].x - 8.5f, mapHandler.enemyPath[pathIndex].y - 4.5f);
        pathIndex++;
        target = new Vector2(mapHandler.enemyPath[pathIndex].x - 8.5f, mapHandler.enemyPath[pathIndex].y - 4.5f);
    }
    
    void Update()
    {
        rb.transform.position = Vector2.MoveTowards(rb.transform.position, target, 1f * Time.deltaTime);
        rb.transform.position += new Vector3(0f, 0f, -2f);

        if ((Vector2)transform.position == target && pathIndex == mapHandler.enemyPath.Count - 1)
        {
            Destroy(gameObject);
        }
        else if ((Vector2)transform.position == target)
        {
            pathIndex++;
            target = new Vector2(mapHandler.enemyPath[pathIndex].x - 8.5f, mapHandler.enemyPath[pathIndex].y - 4.5f);
        }
    }
}
