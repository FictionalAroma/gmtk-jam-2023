using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Hooks : MonoBehaviour
{
    public bool IsConnected => _joint != null;
    Joint _joint;
    private Collider _collider;
    PlayerController _playerController;
    Grappling grappling;
    // Start is called before the first frame update
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }
    void Start()
    {
        grappling = FindObjectOfType<Grappling>();
        _playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ShootHook(float angle, Vector3 direction, float hookshotPower,ForceMode forceMode)
    {
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        this.gameObject.GetComponent<Rigidbody>().AddForce(direction * grappling.grappleHookPower, ForceMode.Impulse);
        _joint = this.gameObject.AddComponent<FixedJoint>();
        _collider.enabled = true;
    }
    public void Disconnect()
    {
        Destroy(_joint);
        _joint = null;


    }
    public void Connect(Rigidbody actor)
    {

        _joint.connectedBody = actor;
        _joint.connectedMassScale = 0f;
        _collider.enabled = false;
        _playerController.PullToHook(this.gameObject);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hookable"))
        {
            Connect(collision.gameObject.GetComponent<Rigidbody>());
        }
    }
}
