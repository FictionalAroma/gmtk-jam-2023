using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject grappleHookHead;
    public float grappleHookPower;
    GameObject player;
    void Start()
    {
        this.GetComponent<Rigidbody>().AddForce(Vector3.forward * grappleHookPower, ForceMode.Impulse);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hookable"))
        {
            this.transform.position = collision.GetContact(0).point;
            player.GetComponent<InputTest>().MoveToGrapple(this.transform.position);
        }
    }


}
