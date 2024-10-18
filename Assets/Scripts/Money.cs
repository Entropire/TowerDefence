using System;
using System.Diagnostics;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Money : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private int money;
    [SerializeField] private int[] towerCosts;
    [SerializeField] private int[] EnemieWorth;

    private readonly string[] enemyNameList = new []{ "Normal", "Speedy", "Tank", "Healer", "Boss" };

    private void Start()
    {
        EnemyHealth.onEnemyDeath += HandleEnemyDeathEvent;
    }

    public bool BuyTower(int i)
    {
        if (money - towerCosts[i] >= 0)
        {
            money -= towerCosts[i];
            moneyText.text = $"Moeny: {money}";
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SellTower(int i)
    {
        money += (int)Mathf.Round(towerCosts[i] / 2);
        moneyText.text = $"Moeny: {money}";
    }

    public void RefundTower(int i)
    {
        money += towerCosts[i];
        moneyText.text = $"Moeny: {money}";
    }

    public void HandleEnemyDeathEvent(GameObject enemy)
    {

        int enemyIndex = 0; 

        for (int i = 0; i < enemyNameList.Length; i++)
        {
            if (enemy.name.Contains(enemyNameList[i]))
            {
                enemyIndex = i;
                break;
            }
        }

        Debug.LogWarning(enemyIndex);
        
        money += EnemieWorth[enemyIndex];
        moneyText.text = $"Moeny: {money}";
    }
}