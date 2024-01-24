using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<Transform> waypoints;
    private int _round, _enemiesSpawned, _enemyCount;
    public Action<int, int, int> UpdateUI;
    private void Start()
    {
        SpawnEnemy();
        UpdateUI(_round, _enemiesSpawned, _enemyCount);
        _enemyCount = Mathf.RoundToInt(2 * Mathf.Pow(5, _round));
    }


    
    private async void SpawnEnemy()
    {
        GameObject tempEnemy = Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity);
        EnemyController tempController = tempEnemy.GetComponent<EnemyController>();
        tempController.SetList(waypoints);
        await Task.Delay(500);
        if (!this.IsDestroyed() && _enemiesSpawned < _enemyCount)
        {
            _enemiesSpawned++;
            SpawnEnemy();
        }else if (_enemiesSpawned >= _enemyCount)
        {
            _round++;
            _enemyCount = Mathf.RoundToInt(2 * Mathf.Pow(5, _round));
            _enemiesSpawned = 0;
            await Task.Delay(5000);
            SpawnEnemy();
        }

        UpdateUI(_round, _enemiesSpawned, _enemyCount);
    }
}
