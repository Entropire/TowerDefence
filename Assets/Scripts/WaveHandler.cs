using UnityEngine;

public class WaveHandler : MonoBehaviour
{
    public GameObject enemyPrefab;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(enemyPrefab);
        }
    }
}
