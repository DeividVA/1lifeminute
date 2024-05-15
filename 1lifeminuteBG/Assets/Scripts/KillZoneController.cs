using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillZoneController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            TileMapManager.Instance.ResetPoints();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

