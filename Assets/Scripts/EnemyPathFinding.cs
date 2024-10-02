using DefaultNamespace;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    public EnemyPath enemyPath;
    private Vector2 target;
    private int pathIndex;

    void Start()
    {
        transform.position = new Vector2(enemyPath.path[pathIndex].x - 8.5f, enemyPath.path[pathIndex].y - 4.5f);
        pathIndex++;
        target = new Vector2(enemyPath.path[pathIndex].x - 8.5f, enemyPath.path[pathIndex].y - 4.5f);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, 1f * Time.deltaTime);
        transform.position += new Vector3(0f, 0f, -2f);

        if ((Vector2)transform.position == target && pathIndex == enemyPath.path.Count - 1)
        {
            Destroy(gameObject);
        }
        else if ((Vector2)transform.position == target)
        {
            pathIndex++;
            target = new Vector2(enemyPath.path[pathIndex].x - 8.5f, enemyPath.path[pathIndex].y - 4.5f);
        }
    }
}