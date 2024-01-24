using System;
using UnityEngine;

namespace Towers
{
    public class Wall : MonoBehaviour
    {
        public Action updateTower;
        public int health = 2;
    

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("Hit");
            if (other.gameObject.CompareTag("Enemy"))
            {
            
                EnemyController tempController = other.gameObject.GetComponent<EnemyController>();
                tempController.moveSpeed = tempController.moveSpeed / 2;
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                EnemyController tempController = other.gameObject.GetComponent<EnemyController>();
                tempController.moveSpeed = tempController.initialMoveSpeed;
            }
        }
    }
}
