using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    public float speed = 5f;
    public float arrivalDistance = 0.1f;
    public float waitTime = 2f; // Adjust this value as needed

    private int currentWaypointIndex = 0;
    private int direction = 1;
    private float waitTimer = 0f;
    private bool isWaiting = false;

    void FixedUpdate()
    {
        if (waypoints.Length == 0)
        {
            return; // Don't proceed if there are no waypoints
        }

        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                isWaiting = false;
                
                currentWaypointIndex += direction;

                if (currentWaypointIndex >= waypoints.Length || currentWaypointIndex < 0)
                {
                    direction *= -1; // Reverse the direction when reaching the end or start
                    currentWaypointIndex += direction * 2; // Move two steps in the opposite direction
                }
            }
        }
        else if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].transform.position) < arrivalDistance)
        {
            isWaiting = true;
            waitTimer = waitTime;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, speed * Time.deltaTime);
        }
    }
}