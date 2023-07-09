using Management;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] GameObject dialogueSFXManager;
    [SerializeField] GameObject sfxOuterManager;
    [SerializeField] GameObject sfxInnerManager;
    [SerializeField] GameObject ambienceManager;
    [SerializeField] GameObject playerSFXManager;
    [SerializeField] AudioClip[] playerSFXClips;
    [SerializeField] AudioClip[] dialogueClips;
    [SerializeField] AudioClip[] sfxOuterClips;
    [SerializeField] AudioClip[] sfxInnerClips;
    [SerializeField] AudioClip[] ambienceClips;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayDialogue()
    {

    }
    public void PlayOuterSFX(AudioClip sFX)
    {
        if (!sfxOuterManager.GetComponent<AudioSource>().isPlaying)
        {
            sfxOuterManager.GetComponent<AudioSource>().PlayOneShot(sFX);
        }
        else
        {
            sfxOuterManager.GetComponent<AudioSource>().Stop();
            sfxOuterManager.GetComponent<AudioSource>().PlayOneShot(sFX);
        }
   
    }
    public void PlayInnerSFX(AudioClip sFX)
    {
        if(!sfxInnerManager.GetComponent<AudioSource>().isPlaying)
        {
            sfxInnerManager.GetComponent<AudioSource>().PlayOneShot(sFX);
        }
        else
        {
            sfxInnerManager.GetComponent<AudioSource>().Stop();
            sfxInnerManager.GetComponent<AudioSource>().PlayOneShot(sFX);
        }
    }
    public void PlayPlayerSFX(AudioClip playerSFX)
    {
        if(!playerSFXManager.GetComponent<AudioSource>().isPlaying)
        {
            playerSFXManager.GetComponent<AudioSource>().PlayOneShot(playerSFX);
        }
        else
        {
            playerSFXManager.GetComponent<AudioSource>().Stop();
            playerSFXManager.GetComponent<AudioSource>().PlayOneShot(playerSFX);
        }
    }
}
