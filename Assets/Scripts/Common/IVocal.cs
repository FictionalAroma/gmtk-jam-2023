using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVocal 
{
    public AudioClip[] AudioClips { get; set; }  
    public AudioSource AudioSource { get; set; }
    public AudioClip ChosenClip { get; set; }
    public float Volume { get; set; }
    void PlaySound();

    void StopSound();
    
    
}
