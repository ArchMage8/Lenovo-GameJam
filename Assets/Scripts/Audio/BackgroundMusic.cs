using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [HideInInspector]public bool ChasingMusic = false;
    [Header("Adjust Transition Speed ( Lower = Smoother )")] 
    [Range(0.01f, 2f)]public float InAudiofade = 5f;
    [Range(0.01f, 2f)]public float OutAudiofade = 5f;
    public AudioSource ChaseMusic;
    public AudioSource SneakMusic;
    public AudioClip ChaseTrigger;

    private bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ChasingMusic)
        {
            if (ChaseMusic.volume >= 0)
            {
                ChaseMusic.volume -= OutAudiofade;
            }
            if(!SneakMusic.isPlaying)
            {
                SneakMusic.Play();
            }
            if(ChaseMusic.volume <= 0.1 && SneakMusic.volume <= 1){
                isPlaying = true;   
                SneakMusic.volume += InAudiofade;
                ChaseMusic.Stop();
            }
        }
        else
        {
            if (SneakMusic.volume >= 0)
            {
                SneakMusic.volume -= OutAudiofade;
            }
            if(!ChaseMusic.isPlaying)
            {
                ChaseMusic.Play();
            }
            if(SneakMusic.volume <= 0.1 && ChaseMusic.volume <= 1){
                isPlaying = false;
                ChaseMusic.volume += InAudiofade;
                SneakMusic.Stop();
            }
        }

        if (ChasingMusic && !isPlaying) 
        {
            SoundManager.instance.PlaySound(ChaseTrigger);
        }
        

  }
}

