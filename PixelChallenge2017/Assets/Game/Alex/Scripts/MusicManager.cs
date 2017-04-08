using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : PublicSingleton<MusicManager> {

    public AudioSource speaker;

    public List<AudioClip> musicList = new List<AudioClip>();

    public int currentSong;

    void Start()
    {
        if (musicList.Count < 1)
            return;
        currentSong = 0;
        speaker.clip = musicList[currentSong];
        speaker.Play();
    }

    public void OnClick()
    {
        if (musicList.Count < 1)
            return;
        speaker.Stop();
        currentSong++;
        if (currentSong >= musicList.Count)
            currentSong = 0;
        speaker.clip = musicList[currentSong];
        speaker.Play();
    }
}
