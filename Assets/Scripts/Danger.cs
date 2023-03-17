using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Danger : MonoBehaviour
{
    [SerializeField] private bool _destroysCargo;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //reboot/respawn
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } else if (_destroysCargo && (other.CompareTag("Cargo") || other.CompareTag("SuperCargo")))
        {
            Destroy(other.gameObject);
        }
    }
}
