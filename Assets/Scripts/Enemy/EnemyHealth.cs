using System;
using Unity.Mathematics;
using UnityEngine;


public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] SpriteRenderer spriteRenderer;
    
    public static event Action<GameObject> onEnemyDeath;
    
    private int health;

    public void TakeDamege(int damage)
    {
        health -= damage;
        float procentHealth = (float)health / maxHealth;
        procentHealth = procentHealth < 0 ? 0 : procentHealth;
        Debug.Log($"health: {health}, maxhealth: {maxHealth}, procentHealth: {procentHealth}");
        spriteRenderer.color = Color.Lerp(Color.red, Color.green, procentHealth);
        if (health <= 0)
        {
            onEnemyDeath?.Invoke(gameObject);
            Destroy(gameObject);
        }   
    }

    private void Start()
    {
        health = maxHealth;
    }
}