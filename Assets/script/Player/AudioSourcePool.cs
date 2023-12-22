using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourcePool : MonoBehaviour
{
    public static AudioSourcePool Instance { get; private set; }

    [SerializeField]
    private AudioSource audioSourcePrefab; // Assignez cela dans l'inspecteur
    [SerializeField]
    private int poolSize = 10;

    private Queue<AudioSource> pool = new Queue<AudioSource>();

    private void Awake()
    {
        Instance = this;

        // Initialisez le pool
        for (int i = 0; i < poolSize; i++)
        {
            AudioSource source = Instantiate(audioSourcePrefab, transform);
            source.gameObject.SetActive(false);
            pool.Enqueue(source);
        }
    }

    public AudioSource GetAudioSource()
    {
        if (pool.Count > 0)
        {
            AudioSource source = pool.Dequeue();
            source.gameObject.SetActive(true);
            return source;
        }

        Debug.LogWarning("Plus d'AudioSources disponibles, envisagez d'augmenter la taille du pool.");
        return null;
    }

    public void ReturnAudioSource(AudioSource source)
    {
        source.gameObject.SetActive(false);
        pool.Enqueue(source);
    }
}
