using UnityEngine;

public class TowerHolder : MonoBehaviour
{
    public bool occupiedSpot;
    public GameObject tower;
    public int value;
    [SerializeField] private GameObject createHighlight;

    public void StartCreate()
    {
        if (!occupiedSpot)
        {
            createHighlight.SetActive(true);
        }
    }

    public void EndCreate()
    {
        createHighlight.SetActive(false); 
    }
    
    public void SellTower()
    {
        Destroy(tower);
        tower = null;
        occupiedSpot = false;
        value = 0;
    }
}
