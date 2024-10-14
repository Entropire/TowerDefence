using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    
    private int health = 20;

    private void Start()
    {
        EnemyPathFinding.OnEndReached += HandleDamageEvent;
        healthText.text = $"Heath: {health}";
    }

    private void HandleDamageEvent(GameObject gameObject)
    {
        health--;
        healthText.text = $"Heath: {health}";
        
        if (health <= 0)
        {
            SceneManager.LoadScene(2);
        }
    }
}