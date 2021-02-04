using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMover : MonoBehaviour
{
    public Transform Platform;
    public List<Transform> Waypoints;

    public float MovementSpeed = 5.0f;
    public float WaitAtWaypointTime = 2.5f;

    public bool ShouldLoop = true;

    private int _currentWaypointIndex = 0;

    private float _timer = 0.0f;

    [SerializeField] private bool _shouldMove = true;

    private void Awake()
    {
        // javna varijabla za index pocetne pozicije
        Platform.position = Waypoints[0].position;
    }

    private void Update()
    {
        if((_shouldMove) && (Time.time >= _timer))
            MovePlatformTowardsCurrentWaypoint();
    }

    private void MovePlatformTowardsCurrentWaypoint()
    {
        Platform.position = Vector3.MoveTowards(Platform.position, Waypoints[_currentWaypointIndex].position, MovementSpeed * Time.deltaTime);

        float distance = Vector3.Distance(Platform.position, Waypoints[_currentWaypointIndex].position);

        if (distance <= 0.0f)
        {
            _currentWaypointIndex++;
            _timer = Time.time + WaitAtWaypointTime;

            if (_currentWaypointIndex >= Waypoints.Count)
            {
                if (ShouldLoop)
                    _currentWaypointIndex = 0;
                else
                    _shouldMove = false;
            }
        }
    }
}
