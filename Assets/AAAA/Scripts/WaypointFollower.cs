using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    int currentWaypointIndex = 0;
    int direction = 1; // 1 for forward, -1 for backward

    [SerializeField] float speed = 1f;

    void FixedUpdate()
    {
        if (waypoints.Length == 0)
        {
            return; // Don't proceed if there are no waypoints
        }

        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < .1f)
        {
            currentWaypointIndex += direction;

            if (currentWaypointIndex >= waypoints.Length || currentWaypointIndex < 0)
            {
                direction *= -1; // Reverse the direction when reaching the end or start
                currentWaypointIndex += direction * 2; // Move two steps in the opposite direction
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
    }
}