using System;
using UnityEngine;

public class TowerPlacing : MonoBehaviour
{
    public static event Action<GameObject> OnTowerPlace;
    
    private Grid grid;
    private Vector2Int cellSize;

    private GameObject selectedTower;
    
    private void Update()
    {
        if (selectedTower)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            selectedTower.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
            
            if (Input.GetMouseButtonDown(0))
            {
                OnTowerPlace?.Invoke(selectedTower);
                selectedTower = null;
            }
        }
    }
    
    public void SelectTower(GameObject tower)
    {
        if (selectedTower)
        {
            Destroy(selectedTower);
        }
        
        selectedTower = Instantiate(tower);
    }
}
