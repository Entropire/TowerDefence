using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    
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
}
