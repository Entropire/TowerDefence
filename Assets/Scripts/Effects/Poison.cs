using UnityEngine;

public class Poison : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private int interval = 1;
    [SerializeField] private int duration = 5;

    private EnemyHealth enemyHealth;
    private float timer;
    private float nextInterval;

    private void Start()
    {
        if (TryGetComponent(out enemyHealth))
        {
            Debug.LogWarning("Can not deal damage to a object with out health!");
        }
    }

    private void Update()
    {

        if (timer >= nextInterval)
        {
            nextInterval += interval;
            if (enemyHealth)
            {
                enemyHealth.TakeDamege(damage);
            }
        }

        if (timer >= duration)
        {
            Destroy(this);
        }
        
        timer += Time.deltaTime;
    }
}