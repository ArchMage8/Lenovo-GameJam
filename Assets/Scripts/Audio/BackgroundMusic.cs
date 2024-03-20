using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public bool ChasingMusic = false;
    public float audiofade = 5f;
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
                SneakMusic.volume +=audiofade;
                ChaseMusic.volume -=audiofade;
            }
        }
        else
        {
            if(SneakMusic.volume >= 0)
            {
                SneakMusic.volume -= audiofade;
                ChaseMusic.volume += audiofade;
            }
        }
    }
}
