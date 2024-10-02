using UnityEngine;

public class TowerPlaceUI : MonoBehaviour
{
    [SerializeField] private GameObject hotBar;
    [SerializeField] private GameObject visualGrid;
    
    void Start()
    {
        TowerPlacing.OnTowerSelected += ShowUI;
        TowerPlacing.OnTowerDeselected += HideUI;
    }

    private void HideUI()
    {
        hotBar.SetActive(true);
        visualGrid.SetActive(false);
    }

    private void ShowUI()
    {
        hotBar.SetActive(false);
        visualGrid.SetActive(true);
    }
}
