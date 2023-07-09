using Management;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum PlayerAudioClips
    {
        jetpackSFX,
        grapplinghookSFX,
        weldingSFX,
        fireExtinguisherSFX
    }
    public enum OuterAudioClips
    {
        laserSFX,
        flybySFX,
        engineOffSFX,
        engineOnSFX

    }
    public enum InnerAudioClip
    {
        powerOffSFX,
        powerOnSFX,
        fireSFX,
        metalCrackSFX,
        lockonAlarmSFX
    }
    [SerializeField] GameObject dialogueSFXManager;
    [SerializeField] GameObject sfxOuterManager;
    [SerializeField] GameObject sfxInnerManager;
    [SerializeField] GameObject ambienceManager;
    [SerializeField] GameObject playerSFXManager;
    [SerializeField] AudioClip[] playerSFXClips;
    [SerializeField] AudioClip[] dialogueClips;
    [SerializeField] AudioClip[] sfxOuterClips;
    [SerializeField] AudioClip[] sfxInnerClips;
    [SerializeField] AudioClip ambienceClip;
    public Dictionary<InnerAudioClip, AudioClip> innerAudioClips;
    public Dictionary<OuterAudioClips, AudioClip> outerAudioClips;
    public Dictionary<PlayerAudioClips, AudioClip> playerAudioClips;
    public bool stillSpeaking;
    // Start is called before the first frame update
    private static AudioManager instance { get; set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        innerAudioClips = new Dictionary<InnerAudioClip, AudioClip>();
        outerAudioClips = new Dictionary<OuterAudioClips, AudioClip>();
        playerAudioClips = new Dictionary<PlayerAudioClips, AudioClip>();
        
    }
    void Start()
    {
        innerAudioClips.Add(InnerAudioClip.powerOffSFX, sfxInnerClips[2]);
        innerAudioClips.Add(InnerAudioClip.lockonAlarmSFX, sfxInnerClips[1]);
        innerAudioClips.Add(InnerAudioClip.powerOnSFX, sfxInnerClips[0]);
        innerAudioClips.Add(InnerAudioClip.fireSFX, sfxInnerClips[4]);
        innerAudioClips.Add(InnerAudioClip.metalCrackSFX, sfxInnerClips[3]);

        outerAudioClips.Add(OuterAudioClips.flybySFX, sfxOuterClips[2]);
        outerAudioClips.Add(OuterAudioClips.laserSFX, sfxOuterClips[0]);
        outerAudioClips.Add(OuterAudioClips.engineOffSFX, sfxOuterClips[3]);
        outerAudioClips.Add(OuterAudioClips.engineOnSFX, sfxOuterClips[1]);

        playerAudioClips.Add(PlayerAudioClips.fireExtinguisherSFX, playerSFXClips[1]);
        playerAudioClips.Add(PlayerAudioClips.jetpackSFX, playerSFXClips[0]);
        playerAudioClips.Add(PlayerAudioClips.weldingSFX, playerSFXClips[2]);
        playerAudioClips.Add(PlayerAudioClips.grapplinghookSFX, playerSFXClips[3]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayAmbience()
    {
        ambienceManager.GetComponent<AudioSource>().clip = ambienceClip;
        ambienceManager.GetComponent<AudioSource>().Play();
    }
    public void PlayDialogue()
    {

    }
    /*public IEnumerator SpeakDialogue()
    {
        stillSpeaking = true;
        while (stillSpeaking)
        {
            dialogueSFXManager.GetComponent<AudioSource>().Stop();
            int randomClip = Random.Range(0, dialogueClips.Length - 1);
            dialogueSFXManager.GetComponent<AudioSource>().clip = dialogueClips[randomClip];
            dialogueSFXManager.GetComponent<AudioSource>().Play();
            while(dialogueSFXManager.GetComponent<AudioSource>().isPlaying)
            {
                yield return null;
                if (stillSpeaking == false)
                {
                    break;
                }
            }
        }
    }*/
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
            sfxInnerManager.GetComponent<AudioSource>().clip = sFX;
            sfxInnerManager.GetComponent<AudioSource>().Play();
        }
       
    }
    public void StopInnerSFX()
    {
        sfxInnerManager.GetComponent<AudioSource>().Stop();
    }
    public void PlayPlayerSFX(AudioClip playerSFX)
    {
        if(!playerSFXManager.GetComponent<AudioSource>().isPlaying)
        {
            playerSFXManager.GetComponent <AudioSource>().clip = playerSFX;
            playerSFXManager.GetComponent<AudioSource>().Play();
        }
        else
        {
            playerSFXManager.GetComponent<AudioSource>().Stop();
            playerSFXManager.GetComponent<AudioSource>().clip = playerSFX;
            playerSFXManager.GetComponent<AudioSource>().Play();
        }
    }
    public void StopPlayerSFX()
    {
        playerSFXManager.GetComponent<AudioSource>().Stop();
        playerSFXManager.GetComponent<AudioSource>().clip = null;

    }
}
