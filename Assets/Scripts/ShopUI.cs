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
        resourceManager.updateUI += UpdateResources;
        enemySpawner.UpdateUI += UpdateEnemyStats;
    }
    //Issue is that when you pick the item in the shop, it does not select the right tower. It seems like a good idea to have separate menus for production and shooting towers
    //Alternatively, I'll need to find a better way of selecting the tower. This could also be done by adding an identifier in the SO script.
    //I really hope I remember to check this...
    public void PickedTower(GameObject thisButton)
    {
        towerManager.selectedTower = thisButton.transform.GetSiblingIndex()-1;
        towerManager.InitiateCreateMode();
        cancelBuild.SetActive(true);
    }
    
    public void CancelBuild()
    {
        towerManager.CompleteCreateMode();
        cancelBuild.SetActive(false);
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
