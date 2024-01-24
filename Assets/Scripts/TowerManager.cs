using System;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] towers;
    [SerializeField] private GameObject allHolders;
    public bool createMode;
    public int selectedTower;
    private GameObject _currentObject;
    private TowerHolder _currentHolder;
    private MeshRenderer _currentRenderer;
    public Action TowerManagementInteraction, CloseTab, InitiateCreateMode, CompleteCreateMode;
    private Action _createTowerCallback; 
    [SerializeField] private ResourceManager resourceManager;

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
        _currentHolder.occupiedSpot = true;
        _currentHolder.tower = Instantiate(towers[selectedTower], _currentHolder.transform.position, Quaternion.identity);
        EndCreateMode();
    }
}
