using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    
    public GameObject target;
    
    void FixedUpdate()
    {
        if (target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.fixedDeltaTime);
            transform.position = new(transform.position.x, transform.position.y, -1);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("test");
        if (other.gameObject == target)
        {
            if (other.TryGetComponent(out EnemyHealth enemyData))
            {
                enemyData.Health -= damage;
                Destroy(gameObject);
            }
        }
    }
}
