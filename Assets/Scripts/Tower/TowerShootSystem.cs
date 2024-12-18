using System.Collections.Generic;
using UnityEngine;

public class TowerShootSystem : MonoBehaviour
{
    [SerializeField] private float shootCooldown;
    [SerializeField] private GameObject projectilePrefab;
    
    private List<GameObject> enemiesInRange;
    private float cooldownTimer;
    
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

            if (projectile.TryGetComponent(out ProjectileDamage projectileDamage))
            {
                projectileDamage.target = enemiesInRange[0];
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
        if (other.gameObject.tag == "Enemy" && enemiesInRange.Contains(other.gameObject))
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }
}
