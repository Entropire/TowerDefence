using UnityEngine;

public class TowerPlaceUI : MonoBehaviour
{
    [SerializeField] private GameObject visualGrid;
    
    void Start()
    {
        TowerPlacing.OnTowerSelected += ShowUI;
        TowerPlacing.OnTowerDeselected += HideUI;
    }

    private void HideUI()
    {
        visualGrid.SetActive(false);
    }

    private void ShowUI()
    {
        visualGrid.SetActive(true);
    }
}
