using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public Player player;
    public int damageAmount;

    private Vector3 spawnPoint = new Vector3(9, -42, 168);
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.position = spawnPoint;
            player.TakeDamage(damageAmount);
        }
    }
}
