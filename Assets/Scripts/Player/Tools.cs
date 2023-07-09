using Common;
using UnityEngine;

public class Tools : Pickup
{
    ParticleSystem toolParticle;
    // Start is called before the first frame update
    void Start()
    {
        toolParticle = GetComponent<ParticleSystem>();
    }
	public override void Use()
    {

        toolParticle.Play();
    }
    public override void StopUse()
    {
        toolParticle.Stop();
    }
}
