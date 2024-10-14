using System;
using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    
    public static event Action<GameObject> onEnemyDeath;

    private int health;

    public int Health
    {
        get { return health; }
        set
        {
            health = value;
            if (health <= 0)
            {
                onEnemyDeath?.Invoke(gameObject);
                Destroy(gameObject);
            }
        }
    }
}