using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    // Start is called before the first frame update
    public float grappleHookPower;
    public float grappleHookPull;
    
    [SerializeField] private LineRenderer lineRenderer;
    private PlayerController _player;
	Joint _joint;
	private Rigidbody _rb;
	private Collider _collider;
	[SerializeField] private GameObject[] hands;
	[SerializeField] private GameObject currentHand;
	public bool IsConnected => _joint != null;

	[SerializeField] private Vocal grapplinghookSFX;
	
	

    private void Awake()
	{
		
		_collider = GetComponent<Collider>();
	}

	private void Start()
	{
		grapplinghookSFX = GetComponent<Vocal>();
		_player = FindObjectOfType<PlayerController>();
		StopGrappling();
		if (lineRenderer != null) 
		{
			lineRenderer.startWidth = 0.1f; 
			lineRenderer.endWidth = 0.1f;   
			lineRenderer.startColor = Color.white;
			lineRenderer.endColor = Color.white;
		}
	}

	public void Shoot(Vector3 hookHit, float angle, Vector3 direction)
	{
		
		this.gameObject.SetActive(true);
		grapplinghookSFX.PlaySound();
		
		currentHand = ChooseClosestHand(hookHit);
        currentHand.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _rb = currentHand.GetComponent<Rigidbody>();
        Disconnect();
        _joint = currentHand.AddComponent<FixedJoint>();
        _rb.AddForce(direction * grappleHookPower, ForceMode.Impulse);
		_collider.enabled = true;
		
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
        
        _joint.connectedBody = actor;
        _joint.connectedMassScale = 0f;

		_collider.enabled = false;

    }
	public void StopGrappling()
	{
		// Reset the grapple
		_rb.velocity = Vector3.zero;
		currentHand.transform.position = _player.transform.position;
		grapplinghookSFX.StopSound();
		// Re-parent the hook immediately if it's not in use
		currentHand.transform.parent = _player.transform;

		// Remove the line
		ClearLine();

		this.gameObject.SetActive(false);
	}

	public void Disconnect()
    {
		Destroy(_joint);
		_joint = null ;
        
		
    }
	
    private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Hookable"))
		{
			Connect(collision.gameObject.GetComponent<Rigidbody>());
		}
	}

	private GameObject ChooseClosestHand(Vector3 hookPosition)
	{
		float distance1 = 0;
		float distance2 = 0;

		foreach (GameObject hand in hands)
		{
			float distance = Vector3.Distance(hookPosition, hand.transform.position);
			if (hands[0])
			{
				distance1 = distance;
			}
			else
			{
				distance2 = distance;
			}
		}

		if (distance1>distance2)
		{
			Debug.Log(hands[0]);
			return hands[0];
		}
		else
		{
			Debug.Log(hands[1]);
			return hands[1];
		}
	}

   
}
