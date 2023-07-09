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
    }
    public override void StopUse()
    {
        _toolParticle.Stop();
    }
}
