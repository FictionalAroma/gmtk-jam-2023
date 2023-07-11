using Common;
using UnityEngine;

public class Tools : Pickup , IVocal
{
  
    ParticleSystem _toolParticle;
    public AudioClip[] AudioClips { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public AudioSource AudioSource { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public AudioClip ChosenClip { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float Volume { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    // Start is called before the first frame update
    void Start()
    {
       
        _toolParticle = GetComponent<ParticleSystem>();
    }
	public override void Use()
    {

        _toolParticle.Play();
        if (this.tag =="Welder")
        {
            PlaySound();
        }
        else
        {
            //audioManager.StopPlayerSFX();
            StopSound();
        }
        
    }
    public override void StopUse()
    {
        _toolParticle.Stop();
        
    }

    public void PlaySound()
    {

        for (int i = 0; i < AudioClips.Length; i++)
        {
            if (AudioClips[i].name == ChosenClip.name)
            {
                AudioSource.clip = AudioClips[i];
            }
        }
        AudioSource.Play();


    }

    public void StopSound()
    {
        AudioSource.Stop();
    }
}
