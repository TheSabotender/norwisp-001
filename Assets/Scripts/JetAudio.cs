using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetAudio : MonoBehaviour
{
    public AudioSource source;
    public AudioClip loop;
    public AudioClip end;

    private bool isPlaying;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isPlaying)
        {
            if (source.clip != loop)
                source.clip = loop;

            time += Time.deltaTime;

            if (!source.isPlaying)
            {
                source.loop = true;
                source.Play();
            }
        }
    }

    public void Play()
    {
        isPlaying = true;
    }

    public void Stop()
    {
        isPlaying = false;
        time = 0;

        source.Stop();        
        source.clip = end;
        source.loop = false;
        source.Play();
    }
}
