using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject player;
    public float collectionRange = 2f;
    public TextMeshProUGUI coinCounterText;
    public bool specialCoin;

    // OnTriggerEnter is called when something enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            
            if (distance <= collectionRange)
            {
                if (specialCoin)
                {
                    coinCounterText.text = "∞";
                    Destroy(gameObject);
                }

                if (coinCounterText.text != "∞")
                {
                    coinCounterText.text = (Int32.Parse(coinCounterText.text) + 1).ToString();
                    Destroy(gameObject);
                }
            }
        }
    }
    
    // Draw a Gizmo
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, collectionRange);
    }
}
