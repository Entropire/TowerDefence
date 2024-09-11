using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] towers;
    [SerializeField] private GameObject hotBar;
    [SerializeField] private GameObject tileOutline;

    private GameObject tower; 
    private MapHandler mapHandler;

    private void Start()
    {
        GameObject map = GameObject.FindGameObjectWithTag("Map"); 
        mapHandler = map.GetComponent<MapHandler>(); 
    }

    private void Update()
    {
        if (tower != null)
        {
            tileOutline.SetActive(true);
            hotBar.SetActive(false);

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
            mousePos.z = -1f;
            tower.transform.position = mousePos;

            HandleMouseClick(); 
        }
        else
        {
            tileOutline.SetActive(false);
            hotBar.SetActive(true);
        }
    }

    private void HandleMouseClick()
    {
        if (Input.GetMouseButtonDown(0)) // On left mouse click
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
            Vector3 closestCellPos = mapHandler.GetClosestCell(mousePos, out Cell cell); 

            if (cell != null && !cell.occupied) 
            {
                tower.transform.position = new Vector3(closestCellPos.x, closestCellPos.y, -1f); 
                cell.occupied = true; 
                tower = null; 
            }
        }
    }

    public void CreateTower(int towerIndex)
    {
        if (tower != null) 
        {
            Destroy(tower);
        }
        tower = Instantiate(towers[towerIndex]); 
    }
}