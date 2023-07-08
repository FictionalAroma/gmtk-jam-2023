using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : MonoBehaviour
{
    public float xRotation = 30;
    public float yRotation = 30;
    public float zRotation = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("xRotation",xRotation);
        RenderSettings.skybox.SetFloat("yRotation",yRotation);
        RenderSettings.skybox.SetFloat("zRotation",zRotation);
    }
}
