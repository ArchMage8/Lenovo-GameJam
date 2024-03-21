using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [HideInInspector]public bool ChasingMusic = false;
    [Header("Adjust Transition Speed ( Lower = Smoother )")] 
    [Range(0.01f, 2f)]public float audiofade = 5f;
    public AudioSource ChaseMusic;
    public AudioSource SneakMusic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(ChasingMusic)
        {
            if(SneakMusic.volume <= 1)
            {
                SneakMusic.Play();
                SneakMusic.volume +=audiofade;
                ChaseMusic.volume -=audiofade;
                ChaseMusic.Stop();
            }
        }
        else
        {
            if(SneakMusic.volume >= 0)
            {
                ChaseMusic.Play();
                SneakMusic.volume -= audiofade;
                ChaseMusic.volume += audiofade;
                SneakMusic.Stop();
            }
        }
    }
}
