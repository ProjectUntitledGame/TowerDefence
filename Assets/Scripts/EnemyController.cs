using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public List<Transform> waypoints;
    private int _position;
    private readonly float _rotationSpeed = 25f;
    public float moveSpeed = 5f;
    public readonly float initialMoveSpeed = 5f;
    private readonly float _stoppingDistance = .5f;
    public int health = 2;
    public Action<EnemyController> removeFromSpawnerArray;

    public void SetList(List<Transform> tempWaypoints)
    {
        for (int i = 0; i < tempWaypoints.Count; i++)
        {
            waypoints.Add(tempWaypoints[i]);
        }
    }

    private void Update()
    {
        Move();   
    }

        private void Move()
        {
            if (_position == waypoints.Count)
            {
                Destroy(this.gameObject);
                return;
            }
            Vector3 direction = waypoints[_position].position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
            transform.Translate(Vector3.forward * (moveSpeed * Time.deltaTime));
            if (Vector3.Distance(transform.position, waypoints[_position].position) < _stoppingDistance)
            {
                _position++;
            }
        }
}
