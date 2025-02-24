using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        PlayingEnd();
    }

    private void PlayingEnd()
    {
        if (audioSource.isPlaying) return;
        Destroy(gameObject);
    }
}
