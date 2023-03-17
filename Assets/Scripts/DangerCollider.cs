using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DangerCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //reboot/respawn
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
