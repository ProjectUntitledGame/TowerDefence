using System;
using Towers;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private TowerSO[] towerScriptable;
    [SerializeField] private GameObject allHolders;
    public bool createMode;
    private bool productionTowerSelected;
    public int selectedTower;
    private GameObject _currentObject;
    private TowerHolder _currentHolder;
    private MeshRenderer _currentRenderer;
    public Action TowerManagementInteraction, CloseTab, InitiateCreateMode, CompleteCreateMode;
    private Action _createTowerCallback; 
    [SerializeField] private ResourceManager resourceManager;

    
    //Issue is that when you pick the item in the shop, it does not select the right tower. It seems like a good idea to have separate menus for production and shooting towers
    //Alternatively, I'll need to find a better way of selecting the tower. This could also be done by adding an identifier in the SO script.
    //I really hope I remember to check this...
    
    
    private void Start()
    {
        _createTowerCallback += CreateTower;
        InitiateCreateMode += StartCreateMode;
        CompleteCreateMode += EndCreateMode;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.CompareTag("TowerHolder"))
                {
                    _currentHolder = hit.collider.GetComponent<TowerHolder>();
                    if (_currentHolder.occupiedSpot)
                    {
                        TowerManagementInteraction();
                    }
                    else if(createMode)
                    {
                        resourceManager.Buy(selectedTower, _createTowerCallback, _currentHolder);
                    }
                }
            }
        }
    }

    private void StartCreateMode()
    {
        for (int i = 0; i < allHolders.transform.childCount; i++)
        {
            TowerHolder tempHolder = allHolders.transform.GetChild(i).GetComponent<TowerHolder>();
            tempHolder.StartCreate();
        }

        createMode = true;
    }

    private void EndCreateMode()
    {
        for (int i = 0; i < allHolders.transform.childCount; i++)
        {
            TowerHolder tempHolder = allHolders.transform.GetChild(i).GetComponent<TowerHolder>();
            tempHolder.EndCreate();
        }
        createMode = false;
    }

    
    public void SellTower()
    {
        resourceManager.Refund(_currentHolder.value);
        _currentHolder.SellTower();
        CloseTab();
    }

    private void CreateTower()
    {
        towerScriptable[selectedTower].CreateTower(_currentHolder);
        EndCreateMode();
    }
}
