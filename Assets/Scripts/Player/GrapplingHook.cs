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
	public bool IsConnected => _joint != null;

    public AudioClip[] AudioClips { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public AudioSource AudioSource { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public AudioClip ChosenClip { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float Volume { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Awake()
	{
		_rb = GetComponent<Rigidbody>();
		_collider = GetComponent<Collider>();
	}

	private void Start()
	{
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

	public void Shoot(Vector3 directionToTarget, float angle)
	{
		this.gameObject.SetActive(true);
		PlaySound();
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		_rb.AddForce(directionToTarget * grappleHookPower, ForceMode.Impulse);
		_collider.enabled = true;
		StopSound();
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
        _joint.connectedBody = actor;
        _joint.connectedMassScale = 0f;

		_collider.enabled = false;

    }
	public void StopGrappling()
	{
		// Reset the grapple
		_rb.velocity = Vector3.zero;
		this.transform.position = _player.transform.position;

		// Re-parent the hook immediately if it's not in use
		this.transform.parent = _player.transform;

		// Remove the line
		ClearLine();

		this.gameObject.SetActive(false);
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
		}
	}

    public void PlaySound()
    {

        for (int i = 0; i < AudioClips.Length; i++)
        {
            if (AudioClips[i].name == ChosenClip.name)
            {
                AudioSource.clip = AudioClips[i];
            }
        }
        AudioSource.Play();


    }

    public void StopSound()
    {
        AudioSource.Stop();
    }
}
