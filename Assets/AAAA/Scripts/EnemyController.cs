using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float farRange = 10f;     
    public float closeRange = 2f;    
    public float movementSpeed = 3f;
    
    
    public Transform player;
    public NavMeshAgent navAgent;  

    private bool isChasing = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        navAgent = GetComponent<NavMeshAgent>();

        if (navAgent == null)
        {
            Debug.LogError("NavMeshAgent component not found on the enemy GameObject.");
        }

        if (player == null)
        {
            Debug.LogError("Player GameObject not found or not tagged as 'Player'. Make sure you have a Player GameObject in your scene and it is tagged as 'Player'.");
        }
    }

    void Update()
    {
        if (navAgent == null || player == null)
        {
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < farRange)
        {
            isChasing = true;
            Debug.Log("Chasing...");
            navAgent.SetDestination(player.position);

            if (distanceToPlayer < closeRange)
            {
                // Attack the player or perform attack logic here
                Debug.Log("Attacking...");
            }
        }
        else
        {
            isChasing = false;
            navAgent.SetDestination(transform.position);
        }
    }
}