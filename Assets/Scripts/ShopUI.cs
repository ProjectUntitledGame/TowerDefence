using TMPro;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [Header("Script References")]
    [SerializeField] private TowerManager towerManager;
    [SerializeField] private ResourceManager resourceManager;
    [SerializeField] private EnemySpawn enemySpawner;
    [Header("UI Elements")]
    [SerializeField] private GameObject towerShop, towerManagement, cancelBuild;
    [SerializeField] private TextMeshProUGUI goldDisplay, roundDisplay, killsDisplay;

    private void Awake()
    {
        towerManager.TowerManagementInteraction += ManagementInteraction;
        resourceManager.updateUI += UpdateResources;
        enemySpawner.UpdateUI += UpdateEnemyStats;
    }

    public void PickedTower(GameObject thisButton)
    {
        towerManager.selectedTower = thisButton.transform.GetSiblingIndex();
        towerManager.InitiateCreateMode();
        cancelBuild.SetActive(true);
    }

    public void CancelBuild()
    {
        towerManager.CompleteCreateMode();
        cancelBuild.SetActive(false);
    }

    public void ManagementInteraction()
    {
        towerManagement.SetActive(!towerManagement.activeInHierarchy);
    }

    public void ShopInteraction()
    {
        towerShop.SetActive(!towerShop.activeInHierarchy);
    }

    private void UpdateResources(int gold)
    {
        goldDisplay.text = "Gold: " + gold;
        cancelBuild.SetActive(false);
    }

    private void UpdateEnemyStats(int round, int currentlySpawned, int totalEnemies)
    {
        roundDisplay.text = "Round: " + round;
        killsDisplay.text = "Enemies: " + currentlySpawned + "/" + totalEnemies; 
        
    }
}
