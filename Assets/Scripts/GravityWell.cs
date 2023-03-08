using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWell : MonoBehaviour
{
    public float Power;

    void OnEnable()
    {
        CustomGravity.AddWell(transform, Power);
    }

    void OnDisable()
    {
        CustomGravity.RemoveWell(transform);
    }
}
