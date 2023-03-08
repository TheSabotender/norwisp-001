using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Danger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //reboot/respawn
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } else if (other.CompareTag("Cargo"))
        {
            Destroy(other.gameObject);
        }
    }
}
