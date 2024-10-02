using System.Collections;
using DefaultNamespace;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    [SerializeField] private EnemyPath enemyPath;
    [SerializeField] private GameObject[] enemies;

    private void Start()
    {
        foreach (var enemy in enemies)
        {
            EnemyPathFinding enemyPathFinding = enemy.GetComponent<EnemyPathFinding>();
            enemyPathFinding.enemyPath = enemyPath;
        }
        
        StartCoroutine(WaveSpawner());
    }

    private IEnumerator WaveSpawner()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Instantiate(enemies[0]);
        }
    }
}
