using UnityEngine;

public class Grappling : MonoBehaviour
{
    // Start is called before the first frame update
    public float grappleHookPower;
    public float grappleHookPull;
    
    [SerializeField] private LineRenderer lineRenderer;
    private PlayerController _player;
	
	private Rigidbody _rb;
	
	public GameObject[] hands;
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
		Debug.Log("initiate hook hand");
		//this.gameObject.SetActive(true);
		
		grapplinghookSFX.PlaySound();
		currentHand = ChooseClosestHand(hookHit);
        Debug.Log("Shoot "+currentHand.name);
        currentHand.GetComponent<Hooks>().Disconnect();
		currentHand.GetComponent<Hooks>().ShootHook(angle,direction,grappleHookPower, ForceMode.Impulse);
		
		
	}
	public void ShootHookRayCast(Vector3 raycastDirection)
	{
        Debug.Log("Raycast");
        RaycastHit hookHit;
        Debug.DrawRay(this.transform.position, raycastDirection, Color.green, 2f);
		if (Physics.Raycast(this.transform.position, raycastDirection, out hookHit, Mathf.Infinity))//,LayerMask.NameToLayer("ShipWalls")))
        {
			Debug.Log("hook hit");
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
            //hand.transform.parent = _player.transform;
            hand.GetComponent<Rigidbody>().velocity = Vector3.zero;
			//Debug.Log(hand.name + " hand position " + hand.transform.position+" Needs to go to "+_player.transform.position);
			hand.transform.position = Vector3.zero;//_player.transform.position;
			//Debug.Log(hand.name + " hand updated position " + hand.transform.position);
            grapplinghookSFX.StopSound();
			
        }
		
		// Re-parent the hook immediately if it's not in use
		

		// Remove the line
		ClearLine();

		//this.gameObject.SetActive(false);
	}



    private GameObject ChooseClosestHand(Vector3 hookPosition)
    {
        GameObject closestHand = null;
        float closestDistance = float.MaxValue;
		//Debug.Log("Hook is at " + hookPosition);
        foreach (GameObject hand in hands)
        {
            float distance = Vector3.Distance(hookPosition, hand.transform.position);
			
			//Debug.Log("hand "+hand +" is at "+hand.transform.position+ " and is "+distance+ " away");
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestHand = hand;
            }
        }

        if (closestHand != null)
        {
            Debug.Log("Closest Hand: " + closestHand.name);
        }
        else
        {
            Debug.Log("No hands found.");
        }

        return closestHand;
    }



}
