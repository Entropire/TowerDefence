using System;
using UnityEngine;

public class TowerPlacing : MonoBehaviour
{
    public static event Action<int> OnTowerSelected;
    public static event Action<int> OnTowerDeselected;
    
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject[] towerPrefabs;
    
    private Camera mainCamera;
    private GameObject selectedTower;
    
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (selectedTower)
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            selectedTower.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
            
            if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                DeleteTower();
            }
        }
    }

    private void PlaceTower()
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        
        CellData cellData = grid.GetClosestCell(mousePosition);

        if (cellData != null && !cellData.occupied)
        {
            cellData.occupied = true;
            cellData.TowerData = new TowerData(selectedTower);
            selectedTower.transform.position = cellData.position;
            selectedTower = null;
            OnTowerDeselected?.Invoke(Array.IndexOf(towerPrefabs, selectedTower));        
        }
    }
    
    private void DeleteTower()
    {
        Destroy(selectedTower);
        selectedTower = null;
        OnTowerDeselected?.Invoke(Array.IndexOf(towerPrefabs, selectedTower));
    }

    public void SelectTower(int towerIndex)
    {
        selectedTower = Instantiate(towerPrefabs[towerIndex]);
        selectedTower.name = towerPrefabs[towerIndex].name;
        OnTowerSelected?.Invoke(towerIndex);
    }
}
