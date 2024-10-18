using System.Diagnostics;
using TMPro;
using UnityEngine;
using Button = UnityEngine.UI.Button;

public class TowerUI : MonoBehaviour
{
    [SerializeField] private Money money;
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject towerUI;
    [SerializeField] private RectTransform towerUIContainer;
    [SerializeField] private TMP_Text title;
    [SerializeField] private Button upgradeButtonOne;
    [SerializeField] private Button upgradeButtonTwo;
    [SerializeField] private Button removeButton;
    
    private bool doStuf;
    private Camera mainCamera;
    
    void Start()    
    {
        mainCamera = Camera.main;
        
        TowerPlacing.OnTowerSelected += () => doStuf = false;
        TowerPlacing.OnTowerDeselected += () => doStuf = true;
    }
    
    void Update()
    {
        if (doStuf)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                
                CellData cellData = grid.GetClosestCell(mousePos);

                if (cellData.occupied && cellData.TowerData != null)
                {
                    OpenTowerUI(cellData.TowerData);
                }
                else
                {
                    if (!RectTransformUtility.RectangleContainsScreenPoint(towerUIContainer, Input.mousePosition))
                    {
                        CloseTowerUI();
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                CloseTowerUI();
            }
        }
    }

    private void OpenTowerUI(TowerData towerData)
    {
        towerUI.SetActive(true);

        title.text = towerData.name;
        
        upgradeButtonOne.onClick.RemoveAllListeners();
        upgradeButtonOne.onClick.AddListener(delegate { UpgradeOne(towerData.towerObj); });
        
        upgradeButtonTwo.onClick.RemoveAllListeners();
        upgradeButtonTwo.onClick.AddListener(delegate { UpgradeTwo(towerData.towerObj); });
        
        removeButton.onClick.RemoveAllListeners();
        removeButton.onClick.AddListener(delegate { RemoveTower(towerData.towerObj); });
    }

    public void CloseTowerUI()
    {
        towerUI.SetActive(false);
    }
    
    public void UpgradeOne(GameObject tower)
    {

    }

    public void UpgradeTwo(GameObject tower)
    {

    }

    public void RemoveTower(GameObject tower)
    {
        int towerIndex = 0;

        switch (gameObject.name)
        {
            case "Arrow Tower": towerIndex = 0; break;
            case "Ice Tower": towerIndex = 1; break;
            case "Poison Tower": towerIndex = 2; break;
            case "Fire Tower": towerIndex = 3; break;
            case "Lightning Spire": towerIndex = 4; break;
        }
        
        money.SellTower(towerIndex);
        CellData cellData = grid.GetClosestCell(tower.transform.position);
        cellData.occupied = false;
        cellData.TowerData = null;
        Destroy(tower);
        CloseTowerUI();
    }
}
