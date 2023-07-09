using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    // Start is called before the first frame update
    public float grappleHookPower;
    public float grappleHookPull;
    
    [SerializeField] private LineRenderer lineRenderer;
    private PlayerController _player;
	Joint _joint;
	private void Start()
	{
		_player = FindObjectOfType<PlayerController>();
		
		if (lineRenderer != null) 
		{
			lineRenderer.startWidth = 0.1f; 
			lineRenderer.endWidth = 0.1f;   
			lineRenderer.startColor = Color.white;
			lineRenderer.endColor = Color.white;
		}
	}
	
	void Update() 
	{
		if (lineRenderer != null) 
		{
			lineRenderer.SetPosition(0, transform.position);
			lineRenderer.SetPosition(1, _player.transform.position);
		}
	}
	
	public void ClearLine()
	{
		lineRenderer.SetPosition(0, Vector3.zero);
		lineRenderer.SetPosition(1, Vector3.zero);
		Disconnect();
	}
    public void Connect(Rigidbody actor)
    {
        _joint = gameObject.AddComponent<FixedJoint>();
		this.GetComponent<BoxCollider>().enabled = false;
        _joint.connectedBody = actor;
        _joint.connectedMassScale = 0f;
    }
	public void StopGrappling()
	{
		// Reset the grapple
		this.GetComponent<Rigidbody>().velocity = Vector3.zero;
		this.transform.position = _player.transform.position;

		// Re-parent the hook immediately if it's not in use
		this.transform.parent = _player.transform;

		// Remove the line
		ClearLine();
	}
        public void Disconnect()
    {
        Destroy(_joint);
        _joint = null;
		
    }

    private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Hookable"))
		{

			Connect(collision.gameObject.GetComponent<Rigidbody>());
			_player.MoveToGrapple(transform.position, grappleHookPull);
			_player.isGrappleActive = false;
		}
	}
}
