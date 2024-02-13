using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Towers
{
    public class STLogic : MonoBehaviour
    {
        public TowerSO towerScriptableObject;
        public List<EnemyController> enemiesInRange = new List<EnemyController>();
        private bool noEnemies = true;

        private void Update()
        {
            if (enemiesInRange.Count >= 1)
            {
                Vector3 direction = enemiesInRange[0].transform.position - transform.position;
                Quaternion toRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, towerScriptableObject.turnSpeed * Time.deltaTime);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!enemiesInRange.Contains(other.GetComponent<EnemyController>()))
            {
                EnemyController tempController = other.GetComponent<EnemyController>();
                enemiesInRange.Add(tempController);
                if (noEnemies)
                {
                    Attack();
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            
            if (enemiesInRange.Contains(other.GetComponent<EnemyController>()))
            {
                enemiesInRange.Remove(other.GetComponent<EnemyController>());
            }
        }

        private void Attack()
        {
            EnemyController target = enemiesInRange[0];
            target.health -= towerScriptableObject.damage;
            if (target.health <= 0)
            {
                Destroy(target.gameObject); 
            }
            StartAttack();
        }

        private async Task StartAttack()
        {
            if (enemiesInRange.Count > 0)
            {
                await Task.Delay(1000);
                Attack();
            }
            else
            {
                noEnemies = false;
            }
        }
    }
}
