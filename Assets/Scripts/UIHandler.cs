using UnityEngine;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] towers;
    [SerializeField] private GameObject hotBar;
    [SerializeField] private GameObject tileOutline;

    private GameObject _tower; 
    private MapHandler _mapHandler;
    private Camera _camera;
    
    private void Start()
    {
        GameObject map = GameObject.FindGameObjectWithTag("Map"); 
        _mapHandler = map.GetComponent<MapHandler>(); 
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_tower)
        {
            tileOutline.SetActive(true);
            hotBar.SetActive(false);

            Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition); 
            mousePos.z = -1f;
            _tower.transform.position = mousePos;

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
        if (!Input.GetMouseButtonDown(0)) return;
        Vector3 mousePos = _camera.ScreenToWorldPoint(Input.mousePosition); 
        Vector3 closestCellPos = _mapHandler.GetClosestCell(mousePos, out Cell cell);

        if (!cell || cell.occupied) return;
        _tower.transform.position = new Vector3(closestCellPos.x, closestCellPos.y, -1f); 
        cell.occupied = true; 
        _tower = null;
    }

    public void CreateTower(int towerIndex)
    {
        if (_tower != null) 
        {
            Destroy(_tower);
        }
        _tower = Instantiate(towers[towerIndex]); 
    }
}