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
    private void Update()
    {
        
    }

    public void UseTool()
    {

        toolParticle.Play();
    }
    public void StopUseTool()
    {
        toolParticle.Stop();
    }
}
