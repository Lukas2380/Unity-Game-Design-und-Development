using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    public TextMeshProUGUI coinCounterText;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Change the tag to match the object you want to teleport.
        {
            PlayerPrefs.SetString("HighScore", coinCounterText.text);
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
