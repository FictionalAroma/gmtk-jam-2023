using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Hooks : MonoBehaviour
{
    public bool IsConnected => _joint != null;
    [SerializeField]Joint _joint;
    [SerializeField]private Collider _collider;
    [SerializeField] PlayerController _playerController;
    [SerializeField]Grappling grappling;
    // Start is called before the first frame update
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }
    void Start()
    {
        //grappling = FindObjectOfType<Grappling>();
        //_playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void ShootHook(float angle, Vector3 direction, float hookshotPower,ForceMode forceMode)
    {
        this.transform.parent = null;
        this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        this.GetComponent<Rigidbody>().AddForce(direction * grappling.grappleHookPower, ForceMode.Force);
        //_joint = this.GetComponent<FixedJoint>();
        _collider.enabled = true;
    }
    public void Disconnect()
    {
        //Destroy(_joint);
        //_joint = null;
        _joint.connectedBody = null;

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
