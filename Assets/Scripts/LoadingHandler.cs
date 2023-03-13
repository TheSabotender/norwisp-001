using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Load", 1.0f);
    }
    void Load()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
