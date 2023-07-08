using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    // Start is called before the first frame update
    public float grappleHookPower;
    public float grappleHookPull;
	PlayerController _player;
    void Start()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Hookable"))
        {
            this.transform.position = collision.GetContact(0).point;
			_player.MoveToGrapple(transform.position,grappleHookPull);
        }
    }

	public void Init(PlayerController playerController)
	{
		_player = playerController;
	}
}
