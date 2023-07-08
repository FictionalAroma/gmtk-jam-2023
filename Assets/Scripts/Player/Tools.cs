using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tools : MonoBehaviour
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
