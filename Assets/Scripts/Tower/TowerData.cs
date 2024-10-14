using UnityEngine;

public class TowerData
{      
      public string name;
      public GameObject towerObj;
      
      public TowerData(GameObject towerObj)
      {
            name = towerObj.name;
            this.towerObj = towerObj;     
      }
}