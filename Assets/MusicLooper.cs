using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class MusicLooper : MonoBehaviour
{
    private AudioSource source;
    private bool playedOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (source.timeSamples <= 44100)
        {
            if (playedOnce)
                source.timeSamples = source.clip.samples / 4;
        }
        else
            playedOnce = true;
    }
}
