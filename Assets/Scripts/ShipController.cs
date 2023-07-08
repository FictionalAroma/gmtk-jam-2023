using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ShipController : MonoBehaviour
{
    public float shipXrotation;
    public float shipYrotation;
    public float shipZrotation;
    SkyboxController skyboxController;
    public float dodgePower;
    public GameObject testEnemy;
    [SerializeField] float ControlPitchFactor = 10f;
    [SerializeField] float controlYawFactor = 5f;
    [SerializeField] float controlRollFactor = 20f;
    // Start is called before the first frame update
    void Start()
    {
        skyboxController = FindObjectOfType<SkyboxController>();
    }

    // Update is called once per frame
    void Update()
    {
        //AngleDir(this.transform.forward,testEnemy.transform.forward,this.transform.up);
        //print(AngleDir(this.transform.forward, testEnemy.transform.forward, this.transform.up));
        
        transform.rotation = Quaternion.Euler(shipYrotation*ControlPitchFactor, shipXrotation*controlYawFactor, shipZrotation*controlRollFactor);
        //transform.Rotate(shipXrotation * Time.deltaTime, shipYrotation * Time.deltaTime, shipZrotation * Time.deltaTime);
        if (this.transform.rotation.x > 15 || this.transform.rotation.x < -15)
        {
            shipXrotation = 0;
        }
        if (this.transform.rotation.y > 10 || this.transform.rotation.y < -10)
        {
            shipYrotation = 0;
        }


    }
    //returns -1 when to the left, 1 to the right, and 0 for forward/backward
    /*public float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0.0f)
        {
            return 1.0f;
        }
        else if (dir < 0.0f)
        {
            return -1.0f;
        }
        else
        {
            return 0.0f;
        }
    }*/
    void DetectThreat(GameObject threat, string threatTag)
    {
        if (threatTag == "Asteroid")
        {
            //float dir = AngleDir(this.transform.forward, threat.transform.forward, this.transform.up);
            var dir = (threat.transform.position - this.transform.position).normalized;
            Debug.Log(dir);
            Dodge(dir);
        }
        if (threatTag == "EnemySpaceShip")
        {
            var dir = (threat.transform.position - this.transform.position).normalized;
            Debug.Log(dir);
            Dodge(-dir);
        }
          
    }

    void Turn(float rotation)
    {

    }
    void Dodge(Vector3 direction)
    {
        
        shipZrotation = direction.z*dodgePower;
        shipYrotation = direction.y*dodgePower;
        shipXrotation = direction.x*dodgePower;
      
    }
    void Fire()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        DetectThreat(other.gameObject, other.gameObject.tag);
        
    }
}
