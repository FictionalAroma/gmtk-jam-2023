using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    // Start is called before the first frame update
    public float grappleHookPower;
    public float grappleHookPull;
    GameObject player;
    void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
        this.GetComponent<Rigidbody>().AddForce(Vector3.forward * grappleHookPower, ForceMode.Impulse);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Hookable"))
        {
            this.transform.position = collision.GetContact(0).point;
            player.GetComponent<PlayerController>().MoveToGrapple(this.transform.position,grappleHookPull);
        }
    }


}
