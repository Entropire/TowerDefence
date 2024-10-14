using System.Collections.Generic;
using UnityEngine;

public class TowerShootSystem : MonoBehaviour
{
    [SerializeField] private float shootCooldown;
    [SerializeField] private GameObject projectilePrefab;
    
    [SerializeField] private List<GameObject> enemiesInRange;
    [SerializeField] private float cooldownTimer;
    
    void Start()
    {
        enemiesInRange = new List<GameObject>();
    }

    void Update()
    {
        if (enemiesInRange.Count >= 1 && cooldownTimer <= 0)
        {
            cooldownTimer = shootCooldown;
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            if (projectile.TryGetComponent(out ProjectileMovement projectileMovement))
            {
                projectileMovement.target = enemiesInRange[0];
            }
        }

        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemiesInRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }
}
