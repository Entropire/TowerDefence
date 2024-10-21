using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private int radiusDamage;
    [SerializeField] private float damageRadius;
    [SerializeField] private bool effect;

    private List<GameObject> enemiesInRange;

    public GameObject target;

    private void Start()
    {
        enemiesInRange = new List<GameObject>();    
        EnemyHealth.onEnemyDeath += HandleEnemyDeath;
    }

    private void DealAreaDamage()
    {
        foreach (GameObject enemie in enemiesInRange)
        {
            if (enemie.TryGetComponent(out EnemyHealth enemyHealth))
            {
                enemyHealth.TakeDamege(radiusDamage);
                Destroy(gameObject);
            }
        }
    }

    private void AddEffectToTarget()
    {
        target.AddComponent<Poison>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == target)
        {
            if (other.TryGetComponent(out EnemyHealth enemyData))
            {
                enemyData.TakeDamege(damage); 
                //if(radiusDamage != 0)  DealAreaDamage();
                if(effect) AddEffectToTarget();
                Destroy(gameObject);
            }
        }
        else
        {
            if (other.tag == "Enemy")
            {
                enemiesInRange.Add(other.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy" && enemiesInRange.Contains(other.gameObject))
        {
            enemiesInRange.Remove(other.gameObject);
        }
    }

    private void HandleEnemyDeath(GameObject gameObject)
    {
        if (gameObject && enemiesInRange.Contains(gameObject))
        {
            enemiesInRange.Remove(gameObject);
        }
    }
}
