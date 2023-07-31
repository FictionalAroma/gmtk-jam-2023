using UnityEngine;

public class Grappling : MonoBehaviour
{
    // Start is called before the first frame update
    public float grappleHookPower;
    public float grappleHookPull;
    
    [SerializeField] private LineRenderer lineRenderer;
    private PlayerController _player;
	
	private Rigidbody _rb;
	
	[SerializeField] private GameObject[] hands;
	[SerializeField] private GameObject currentHand;
	

	[SerializeField] private Vocal grapplinghookSFX;
	
	

    private void Awake()
	{
		
		
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
		currentHand.GetComponent<Hooks>().Disconnect();
		currentHand.GetComponent<Hooks>().ShootHook(angle,direction,grappleHookPower, ForceMode.Impulse);
		
		
	}
	public void ShootHookRayCast(Vector3 raycastDirection)
	{
        RaycastHit hookHit;
        Debug.DrawRay(this.transform.position, raycastDirection, Color.green, 2f);
        if (Physics.Raycast(this.transform.position, raycastDirection, out hookHit, Mathf.Infinity, LayerMask.NameToLayer("ShipWalls")))
        {
            float angle = Mathf.Atan2(raycastDirection.y, raycastDirection.x) * Mathf.Rad2Deg;

            Shoot(hookHit.point, angle, raycastDirection);

            _player.isGrappleActive = true;
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
		foreach(GameObject hand in hands)
		{
            hand.GetComponent<Hooks>().Disconnect();
        }
		
	}
    
	public void StopGrappling()
    {
		// Reset the grapple
		foreach (GameObject hand in hands)
		{
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = _player.transform.position;
            grapplinghookSFX.StopSound();
			transform.parent = _player.transform;
        }
		
		// Re-parent the hook immediately if it's not in use
		

		// Remove the line
		ClearLine();

		this.gameObject.SetActive(false);
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
