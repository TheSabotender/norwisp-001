using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonusObject : MonoBehaviour
{
    
    public static Action OnPointScored;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //reboot/respawn
            //just pickup/eat to get points
            Destroy(gameObject);
            OnPointScored?.Invoke();
        }
    }
}