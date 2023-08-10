using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    PlayerController player;
    [SerializeField] float hookPower;
    public bool hookIsFree;
    // Start is called before the first frame update

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }
    public void ShootHook(Vector3 hookPoint)
    {
        this.transform.parent = null;
        this.GetComponent<Collider>().enabled = true;
        this.GetComponent<Rigidbody>().AddForce((hookPoint- this.transform.position)*hookPower, ForceMode.Impulse);
        hookIsFree = false;
    }

    IEnumerator ConnectJoint(Joint joint, Rigidbody objectRb)
    {
        Debug.Log("Connecting Joint");
        this.GetComponent<Rigidbody>().velocity = Vector3.zero;
        joint.connectedBody= objectRb;
        player.PullToHook(this.gameObject);
        yield return new WaitForSeconds(1.5f);
        DisconnectJoint(joint);
    }
    public void DisconnectJoint(Joint joint)
    {
        
        joint.connectedBody = null;
        this.transform.parent = player.gameObject.transform;
        Destroy(joint);
        player.isGrappleActive = false;
        hookIsFree = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hookable"))
        {
            Debug.Log(collision.gameObject.name);
            var joint = this.AddComponent<SpringJoint>();
            StartCoroutine(ConnectJoint(joint,collision.gameObject.GetComponent<Rigidbody>()));
        }
    }
    
}
