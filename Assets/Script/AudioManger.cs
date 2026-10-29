using System;
using UnityEngine;

public class AudioManager: MonoBehaviour
{


   
    public AudioHolder[] Sounds;
    public GameObject AudioSphere;
/*
    private void Update()
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            if (!Sounds[i].Done)
            {
                //this is for how long the clip will be playerd
                Sounds[i].timer -= Time.deltaTime;
                if (Sounds[i].timer <= 0)
                {
                    Sounds[i].Done = true;
                }
                Sounds[i].Interval -= Time.deltaTime;
                if (Sounds[i].Interval <= 0)
                {
                    if (!Sounds[i].Audio.isPlaying)
                    {
                        Sounds[i].Interval = Sounds[i].IntervalContainer;
                        Sounds[i].Audio.Play();
                    }
                }
            }
        }
    }*/

    public void PlaySound(int WhichSound)
    {
        Sounds[WhichSound].Audio.Play();
       // Debug.Log(Sounds[WhichSound].Audio.maxDistance);
        AudioSphere.GetComponent<AudioSphere>().SoundOn(Sounds[WhichSound].Audio.maxDistance);
    }
}

[Serializable]
public class AudioHolder
{


   public  AudioHolder(AudioSource newClip,float NewInterval,float NewTimer,float NoiseLevel=1)
    {
        Audio=newClip;
        Interval=NewInterval;
        timer=NewTimer;
        this.NoiseLevel=NoiseLevel;
        IntervalContainer=Interval;
    }
    public AudioSource Audio;
    public float Interval;
    public float timer;
    public float NoiseLevel;
    public bool Done = false;
    public float IntervalContainer;
}

