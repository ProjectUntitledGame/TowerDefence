using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManagementUI : MonoBehaviour
{
    [SerializeField] private TowerManager towerManager;

    [SerializeField] private GameObject towerManagement;
    // Start is called before the first frame update
    void Start()
    {
        towerManager.TowerManagementInteraction += ManagementInteraction;
    }

    public void ManagementInteraction()
    {
        towerManagement.SetActive(!towerManagement.activeInHierarchy);
    }
}
