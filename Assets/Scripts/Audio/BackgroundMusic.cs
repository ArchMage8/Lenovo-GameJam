using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BackgroundMusic : MonoBehaviour
{
    [HideInInspector]public bool ChasingMusic = false;
    [Header("Adjust Transition Speed ( Lower = Smoother )")] 
    [Range(0.01f, 2f)]public float InAudiofade = 5f;
    [Range(0.01f, 2f)]public float OutAudiofade = 5f;
    public AudioSource ChaseMusic;
    public AudioSource SneakMusic;
    public AudioClip ChaseTrigger;

    public float volume;

    private bool isPlaying = false;

    void Update()
    {
        if (ChasingMusic)
        {
            if(!SneakMusic.isPlaying)
            {
                SneakMusic.Play();
            }
            isPlaying = true;   
            if (SneakMusic.volume <= 1)
            {
                isPlaying = true;   
                SneakMusic.volume += InAudiofade;
                ChaseMusic.volume -= OutAudiofade;
            }
            ChaseMusic.Stop();
        }
        else
        {
            if (SneakMusic.volume >= 0)
            {
                if(!ChaseMusic.isPlaying)
                {
                    ChaseMusic.Play();
                }
                SneakMusic.volume -= InAudiofade;
                ChaseMusic.volume += OutAudiofade;
                SneakMusic.Stop();
                isPlaying = false;
            }
        }

        if (ChasingMusic && !isPlaying) 
        {
            SoundManager.instance.PlaySound(ChaseTrigger, volume);
        }
        

        }
}

