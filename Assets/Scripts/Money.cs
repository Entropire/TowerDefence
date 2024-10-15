using System;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private int money;
    [SerializeField] private int[] towerCosts;
    
    public int moneyValue { get; set; }

    private void Start()
    {
        TowerPlacing.OnTowerSelected += (towerIndex) => money -= towerCosts[towerIndex];
        TowerPlacing.OnTowerDeselected += (towerIndex) => money += towerCosts[towerIndex];
    }
}