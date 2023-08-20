using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyPlatform : MonoBehaviour
{
    public string playerTag = "Player";

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object has the specified tag.
        if (collision.gameObject.CompareTag(playerTag))
        {
            // Reset the player's velocity to prevent unexpected behavior.
            Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                playerRigidbody.velocity = Vector3.zero;
            }

            // Set the platform as the parent of the player's transform.
            collision.gameObject.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            // When the player exits the collision, remove the parent relationship.
            collision.gameObject.transform.SetParent(null);
        }
    }
}