using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text waveIndexTMPText; 
    [SerializeField] private EnemyPath enemyPath;
    [SerializeField] private GameObject[] enemyPrefabs; 
    [SerializeField] private float[] enemyWeights; 
    [SerializeField] private int[] enemyUnlockIndexes;
    
    private List<GameObject> spawnedEnemies; 
    private int[] enemiesToSpawn; 
    private float waveMaxWeight,
                  waveWeight = 0;
    private int unlocktEnemies,
                waveIndex,
                unlockEnemyWaveIndex;
    
    private void Start()
    {
        spawnedEnemies = new List<GameObject>();
        
        EnemyHealth.onEnemyDeath += HandleEnemyDeath;
        EnemyPathFinding.OnEndReached += HandleEnemyDeath;

        foreach (var enemyPrefab in enemyPrefabs)
        {
            EnemyPathFinding enemyPathFinding = enemyPrefab.GetComponent<EnemyPathFinding>();
            enemyPathFinding.enemyPath = enemyPath;
        }
    }

    private void SpawnNextWave()
    {
        if (waveIndex == unlockEnemyWaveIndex)
        {
            unlocktEnemies++;
            unlockEnemyWaveIndex = enemyUnlockIndexes[unlocktEnemies];
        }
        
        waveIndex++;
        waveMaxWeight++;
        waveIndexTMPText.text = $"Wave: {waveIndex}";
        CalculateWave();
        StartCoroutine( SpawnWave());
    }

    private void CalculateWave()
    {
        waveWeight = 0;
        enemiesToSpawn = new int[enemyPrefabs.Length];

        while (waveWeight < waveMaxWeight)
        {
            int i = Mathf.FloorToInt(Random.Range(0, unlocktEnemies));
            enemiesToSpawn[i]++;
            waveWeight += enemyWeights[i];
        }
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemiesToSpawn.Length; i++)
        {
            for (int j = 0; j < enemiesToSpawn[i]; j++)
            {
                GameObject spawnedEnemy = Instantiate(enemyPrefabs[i]);
                spawnedEnemies.Add(spawnedEnemy);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    private void HandleEnemyDeath(GameObject gameObject)
    {
        spawnedEnemies.Remove(gameObject);
        if (spawnedEnemies.Count <= 0) SpawnNextWave();
    }

    public void StartFirstWave(GameObject gameObject)
    {
        SpawnNextWave();
        gameObject.SetActive(false);
    }
}