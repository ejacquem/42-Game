using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMain : MonoBehaviour
{
    public AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayMusic();
    }

    public void Update()
    {
        // Vérifie si la musique a fini de jouer
        if (!audioSource.isPlaying)
        {
            // Relancer la musique (car on aime la musique)
            PlayMusic();
        }
    }

    public void PlayMusic()
    {
        audioSource.Play();
    }
}
