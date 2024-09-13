using UnityEngine;

public class Bullet : MonoBehaviour
{
    internal float speed;
    internal Vector2 target;
    
    private Rigidbody2D rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        rb.transform.position = Vector2.MoveTowards(rb.transform.position, target, speed * Time.deltaTime);
        rb.transform.position += new Vector3(0f, 0f, -2f);
        
        if ((Vector2)rb.transform.position == target)
        {
            Destroy(gameObject);
        }
    }
}
