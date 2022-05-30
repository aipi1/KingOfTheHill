using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    private AudioSource music;

    void Awake()
    {
        music = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (GameUI.isGamePaused)
        {
            music.Pause();

        } else if (!music.isPlaying)
        {
            music.UnPause();
        }
    }
}
