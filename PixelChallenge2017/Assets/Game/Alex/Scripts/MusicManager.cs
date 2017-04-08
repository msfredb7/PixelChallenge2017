using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : PublicSingleton<MusicManager> {

    public AudioSource speaker;
    public AudioSource sfxSpeaker;

    public List<AudioClip> musicList = new List<AudioClip>();
    public AudioClip mouseClick1;
    public AudioClip mouseClick2;

    public int currentSong;

    void Start()
    {
        if (musicList.Count < 1)
            return;
        currentSong = 0;
        speaker.clip = musicList[currentSong];
        speaker.Play();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DoMouseClick1();
        }

        if (Input.GetMouseButtonUp(0))
        {
            DoMouseClick2();
        }
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

    public void DoMouseClick1()
    {
        sfxSpeaker.Stop();
        sfxSpeaker.clip = mouseClick1;
        sfxSpeaker.Play();
    }

    public void DoMouseClick2()
    {
        sfxSpeaker.Stop();
        sfxSpeaker.clip = mouseClick2;
        sfxSpeaker.Play();
    }
}
