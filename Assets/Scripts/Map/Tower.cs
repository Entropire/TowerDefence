using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    
    private List<GameObject> enemies;
    private float shootCooldown = 2f;
    private float timer;
    private MapHandler mapHandler;
    private int bulletSpeed = 2;
    private float enemySpeed = 1f;
    private Vector2 interceptionPos;
    
    private void Start()
    {
        enemies = new List<GameObject>();
        mapHandler = GameObject.Find("Map").GetComponent<MapHandler>();
    }

    private void Update()
    {
        if (timer >= shootCooldown)
        {
            timer = 0f;
            if (enemies.Count > 0)
            {
                int closestPointIndex = GetClosestPointIndex();
            
                Vector2 interceptionPosLocalSpace = mapHandler.enemyPath[closestPointIndex + 1];

                interceptionPos = new(interceptionPosLocalSpace.x - 8.5f, interceptionPosLocalSpace.y - 4.5f);
            
                float distance = Vector3.Distance(interceptionPos, transform.position);
            
                float timeTillInterception = enemySpeed - (distance / bulletSpeed);
            
                Invoke("SpawnBullet", timeTillInterception / 1000f);
            }
        }
        
        timer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        enemies.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
         enemies.Remove(other.gameObject);
    }
    
    internal int GetClosestPointIndex()
    {
        float closestDistance = float.MaxValue;
        int index = 0;
        
        int i = 0;
        foreach (Vector2 vec in mapHandler.enemyPath)
        {
            Vector3 worldPos = new Vector3(vec.x - 8.5f, vec.y - 4.5f);
            float distance = Vector2.Distance(worldPos, enemies[0].transform.position);
            
            Debug.Log($"{i}: " + distance);
            
            if(distance < closestDistance)
            {
                closestDistance = distance;
                index = i; 
            }
            i++;
        }
        
        return index;
    }

    private void SpawnBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Bullet BulletScript = newBullet.GetComponent<Bullet>();
        BulletScript.speed = bulletSpeed;
        BulletScript.target = interceptionPos;
    }
}