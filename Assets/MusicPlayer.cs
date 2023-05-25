using UnityEngine;
using System.Collections.Generic;

public class MusicPlayer : MonoBehaviour
{
    public List<AudioClip> songs;
    private List<AudioClip> unplayedSongs;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ResetUnplayedSongs();
        PlayRandomMusic();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayRandomMusic();
        }
    }

    void ResetUnplayedSongs()
    {
        unplayedSongs = new List<AudioClip>(songs);
    }

    void PlayRandomMusic()
    {
        if (unplayedSongs.Count == 0)
        {
            // Si todas las canciones se han reproducido, reinicia la lista
            ResetUnplayedSongs();
        }

        // Selecciona una canción aleatoria no reproducida
        int randomIndex = Random.Range(0, unplayedSongs.Count);
        AudioClip song = unplayedSongs[randomIndex];

        // Remueve la canción seleccionada de la lista de canciones no reproducidas
        unplayedSongs.RemoveAt(randomIndex);

        // Reproduce la canción seleccionada
        audioSource.clip = song;
        audioSource.Play();
    }
}
