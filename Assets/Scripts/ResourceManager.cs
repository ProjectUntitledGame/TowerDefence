using System;
using UnityEngine;
using TMPro;

public class ResourceManager : MonoBehaviour
{
    public int gold;
    private readonly int _basePrice = 100;
    [SerializeField] private TextMeshProUGUI goldTxtObj;
    public Action<int> updateUI;

    private void Start()
    {
        updateUI(gold);
    }

    private int Cost(float indexPosition)
    {
        int cost = Mathf.RoundToInt(_basePrice * Mathf.Pow(1.2f, indexPosition));
        return cost;
    }

    public void Refund(int value)
    {
        gold += value;
        updateUI(gold);
    }
    
    public void Buy(float indexPosition, Action createTower, TowerHolder towerHolder)
    {
        int cost = Cost(indexPosition);
        if (gold > cost)
        {
            towerHolder.value = cost;
            gold -= cost;
            createTower();
            updateUI(gold);
        }
        else
        {
            Debug.Log("Fucking brokie");
        }
    }
}
