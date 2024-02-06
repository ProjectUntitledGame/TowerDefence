using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Towers
{
    public class ProjectileTower : MonoBehaviour
    {
        public int delay = 1;
        public int damage = 2;
        private readonly float rotationSpeed = 25f;
        public List<EnemyController> enemiesInRange;
        [SerializeField] private GameObject lazer;
        [Header("SlowAttribute")]
        public bool slow;
        public int slowSpeed;
        public Color colourChange;
        private int initialSpeed;
        
    
        private void Start()
        {
            InvokeRepeating("Attack", 0.0f, delay);
        }

        private void Attack()
        {
            if (enemiesInRange.Count >= 1)
            {
                enemiesInRange[0].health -= damage;
                if (slow)
                {
                    SlowEnemies();
                }
                ShootLazer();
                if (enemiesInRange[0].health <= 0)
                {
                    Destroy(enemiesInRange[0].gameObject);
                    enemiesInRange.RemoveAt(0);
                }
            }
        }

        private async void SlowEnemies()
        {
            enemiesInRange[0].moveSpeed = slowSpeed;
            Material tempMaterial = enemiesInRange[0].GetComponent<MeshRenderer>().material;
            Color tempColour = tempMaterial.color;
            tempMaterial.color = colourChange;
            await Task.Delay(500);
            enemiesInRange[1].moveSpeed = initialSpeed;
            tempMaterial.color = tempColour;
        }
        
        
        private async void ShootLazer()
        {
            lazer.SetActive(true);
            await Task.Delay(100);
            lazer.SetActive(false);
        }
    

        private void Update()
        {
            if (enemiesInRange.Count >= 1)
            {
                Vector3 direction = enemiesInRange[0].transform.position - transform.position;
                Quaternion toRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            EnemyController tempController = other.GetComponent<EnemyController>();
            enemiesInRange.Add(tempController);
        }

        private void OnTriggerExit(Collider other)
        {   
            enemiesInRange.RemoveAt(0);
        }
    }
}
