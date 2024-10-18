using System;
using DefaultNamespace;
using UnityEngine;

public class TowerPlacing : MonoBehaviour
{
    public static event Action OnTowerSelected;
    public static event Action OnTowerDeselected;
    
    [SerializeField] private Grid grid;
    [SerializeField] private Money money;
    [SerializeField] private GameObject[] towerPrefabs;
    
    private Camera mainCamera;
    private GameObject selectedTower;
    private int selectedTowerIndex;
    
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
            OnTowerDeselected?.Invoke();        
        }
    }
    
    private void DeleteTower()
    {
        money.RefundTower(selectedTowerIndex);
        selectedTowerIndex = -1;
        Destroy(selectedTower);
        selectedTower = null;
        OnTowerDeselected?.Invoke();
    }

    public void SelectTower(int towerIndex)
    {
        if (money.BuyTower(towerIndex))
        {
            selectedTowerIndex = towerIndex;
            selectedTower = Instantiate(towerPrefabs[towerIndex]);
            selectedTower.name = towerPrefabs[towerIndex].name;
            OnTowerSelected?.Invoke();
        }
  
    }
}
