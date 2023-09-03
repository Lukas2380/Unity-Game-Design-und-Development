using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform movePositionTransform;

    private NavMeshAgent navMeshAgent;
    
    //Attacking
    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public Player player;
    public int damageAmount;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        navMeshAgent.destination = movePositionTransform.position;
        Vector3 targetPosition = new Vector3(movePositionTransform.position.x,
            this.transform.position.y,
            movePositionTransform.position.z);
        this.transform.LookAt(targetPosition);

        if (Vector3.Distance(transform.position, movePositionTransform.position) <= navMeshAgent.stoppingDistance)
        {
            AttackPlayer(); // Aufruf der Angriffsfunktion, wenn die Entfernung zum Ziel gering genug ist
        }
    }

    private void AttackPlayer()
    {
        if (!alreadyAttacked)
        {
            //Attack code
            Debug.Log("Attacking...");
            // Call the TakeDamage method from the Player script
            player.TakeDamage(damageAmount);


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}