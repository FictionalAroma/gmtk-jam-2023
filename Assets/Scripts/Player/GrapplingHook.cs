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
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Hookable"))
		{
			this.transform.position = collision.GetContact(0).point;
			_player.MoveToGrapple(transform.position, grappleHookPull);
			_player.isGrappleActive = false;
		}
	}
}
