using CCC.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : PublicSingleton<MusicManager> {

    public AudioSource speaker;
    public AudioSource sfxSpeaker;
    public AudioSource additionnalSfx;

    public List<AudioClip> musicList = new List<AudioClip>();
    public AudioClip mouseClick1;
    public AudioClip mouseClick2;

    public AudioClip vomit;
    public AudioClip door;
    public AudioClip doorbell;
    public AudioClip whistle;
    public AudioClip coins;

    public int currentSong;

    void Start()
    {
        if (musicList.Count < 1)
            return;
        currentSong = 0;
        speaker.clip = musicList[currentSong];
        speaker.Play();

        RoadManager.instance.onStopReached.AddListener(DoDoorBellSound);
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            DoMouseClick1();
        }
        
        /*
        if (Input.GetMouseButtonUp(0))
        {
            DoMouseClick2();
        }
        */
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

    public void DoDoorBellSound()
    {
        additionnalSfx.Stop();
        if (RoadManager.instance.currentRoad.currentStop.lieu == LieuType.arretBus)
        {
            additionnalSfx.clip = whistle;
            additionnalSfx.Play();
        } else
        {
            additionnalSfx.clip = doorbell;
            DelayManager.CallTo(delegate ()
            {
                additionnalSfx.Play();
            }, 3);
        }
    }

    public void DoCoinsSound()
    {
        additionnalSfx.Stop();
        additionnalSfx.clip = coins;
        additionnalSfx.Play();
    }
}
