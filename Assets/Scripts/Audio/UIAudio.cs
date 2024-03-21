using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudio : MonoBehaviour
{

    public AudioClip selectSound;
    public float selectVolume;
    public AudioClip pressSound;
    public float pressVolume;

    public void SelectSound()
    {
        SoundManager.instance.PlaySound(selectSound, selectVolume);
    }

    public void PressSound() 
    { 
        SoundManager.instance.PlaySound(pressSound, pressVolume);
    }
}
