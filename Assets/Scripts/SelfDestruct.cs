using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("finishHim", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void finishHim()
    {
        Destroy(this.gameObject);
    }
}
