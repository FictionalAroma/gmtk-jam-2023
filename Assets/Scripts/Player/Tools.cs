using Common;
using UnityEngine;

public class Tools : Pickup
{
    AudioManager audioManager;
    ParticleSystem _toolParticle;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType(typeof(AudioManager)) as AudioManager;
        _toolParticle = GetComponent<ParticleSystem>();
    }
	public override void Use()
    {

        _toolParticle.Play();
        if (this.tag =="Welder")
        {
            audioManager.StopPlayerSFX();
            audioManager.PlayPlayerSFX(audioManager.playerAudioClips[AudioManager.PlayerAudioClips.weldingSFX]);
        }
        else
        {
            //audioManager.StopPlayerSFX();
            audioManager.PlayPlayerSFX(audioManager.playerAudioClips[AudioManager.PlayerAudioClips.fireExtinguisherSFX]);
        }
        
    }
    public override void StopUse()
    {
        _toolParticle.Stop();
        audioManager.StopPlayerSFX();
    }
}
