using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroundMover : MonoBehaviour
{
   
    public float MovementSpeed = 1.25f;
    public Transform Enemy;
    public List<Transform> Waypoints;

    private int _currentWaypointIndex = 0;

    private void Update()
    {
        if (Enemy == null)
            return;
        else
        MoveEnemyTowardsCurrentWaypoint();
    }

    private void FlipEnemy()
    {
        Vector3 localScale = Enemy.localScale;
        localScale.x *= -1.0f;
        Enemy.localScale = localScale;
    }

    private void MoveEnemyTowardsCurrentWaypoint()
    {
        AudioManager.instance.Play("Crawling");
        Enemy.position = Vector3.MoveTowards(Enemy.position,
            Waypoints[_currentWaypointIndex].position, MovementSpeed * Time.deltaTime);

        float distance = Vector3.Distance(Enemy.position, Waypoints[_currentWaypointIndex].position);

        if (distance <= 0.0f)
        {
            _currentWaypointIndex++;
            FlipEnemy();
            if (_currentWaypointIndex >= Waypoints.Count)
                _currentWaypointIndex = 0;
        }
    }
}
