using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class Vocal: MonoBehaviour 
{
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip chosenClip;
    [SerializeField] private float volume;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }
    public void PlaySound()
    {
        audioSource.volume = volume;
        audioSource.clip = chosenClip;
        audioSource.Play();
    }

    public void StopSound()
    {
        audioSource.Stop();
    }
    
    
}
