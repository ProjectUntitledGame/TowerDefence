using UnityEngine;

namespace Towers
{
    [CreateAssetMenu(fileName = "NewTower", menuName = "Towers")]
    public class TowerSO : ScriptableObject
    {
        [Header("Initialise")]
        public string towerName;
        public Mesh model;
        public TowerHolder towerHolder;
        public bool productionType;
        [Header("Attack Params")]
        public int range;
        public int turnSpeed;
        public int damage;
        public int delay;
        [Header("Slow Params")]
        public bool slow;
        public float initialSpeed;
        public float slowSpeed;

        
        //Issue is that when you pick the item in the shop, it does not select the right tower. It seems like a good idea to have separate menus for production and shooting towers
        //Alternatively, I'll need to find a better way of selecting the tower. This could also be done by adding an identifier in the SO script.
        //I really hope I remember to check this...
        
        
        public void CreateTower()
        {
            GameObject tower = new GameObject(towerName)
            {
                transform =
                {
                    position = towerHolder.transform.position
                }
                
            };
            
            MeshRenderer tempRenderer = tower.AddComponent<MeshRenderer>();
            MeshFilter tempFilter = tower.AddComponent<MeshFilter>();   
            tempFilter.mesh = model;
            
            STLogic towerLogic = tower.AddComponent<STLogic>();
            towerHolder.occupiedSpot = true;
            towerHolder.tower = tower;
            towerLogic.towerScriptableObject = this;
        }
    }
}

