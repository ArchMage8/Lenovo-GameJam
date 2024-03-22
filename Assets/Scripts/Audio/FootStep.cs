using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStep : MonoBehaviour
{
    private Rigidbody2D rb;
    public AudioSource FootStepAudio;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.x != 0 ||rb.velocity.y != 0)
        {
            if(!FootStepAudio.isPlaying){
                FootStepAudio.Play();
            }
        }
        else
        {
            FootStepAudio.Stop();
        }
    }
}
