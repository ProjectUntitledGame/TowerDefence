using UnityEngine;

namespace Towers
{
    public class SphereTower : MonoBehaviour
    {
        [SerializeField] private GameObject wall;
        private GameObject _path;
        private Wall _currentWall;
        private int _range = 5;
        //private float delay = 5f;

        // Start is called before the first frame update
        void Start()
        {
            _path = FindClosestObject().gameObject;
            CreateWall();
            //InvokeRepeating("CreateWall", 0.0f, delay);
        }

        private void CreateWall()
        {
            if (_currentWall == null)
            {
                GameObject currentWallObj = Instantiate(wall, _path.transform.position, _path.transform.rotation);
                _currentWall = currentWallObj.GetComponent<Wall>();
                _currentWall.updateTower += WallUpdate;
            }        
        }

        private void WallUpdate()
        {
            
        }
    
        GameObject FindClosestObject()
        {
            // Find colliders within range
            Collider[] colliders = Physics.OverlapSphere(transform.position, _range);
            GameObject closestPath = null;
            float closestDistance = float.MaxValue;

            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.CompareTag("Path"))
                {
                    float distance = Vector3.Distance(transform.position, collider.transform.position);

                    if (distance < closestDistance)
                    {
                        closestPath = collider.gameObject;
                        closestDistance = distance;
                    }
                }
            }

            return closestPath;
        }
    }
}
