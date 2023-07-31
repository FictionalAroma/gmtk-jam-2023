using Assets.Scripts.Player;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private float playerspeed = 10f;
    private Vector2 previousAimInput;
    private Vector2 currentMovementInput;
    private Rigidbody _rb;
    

    [Header("Grappling and Tools")]
    [SerializeField] private Grappling grappling;
    private Vocal grapplingHookSFX;
    [SerializeField] GameObject aimIndicator;
    [SerializeField] private float grappleRetractionSpeed = 10f;
    [SerializeField] private float grappleRetractionDelay = .2f;
	[SerializeField] private PlayerPickupController handController;
    public bool isGrappleActive;

    [Header("Jetpack")]
    [SerializeField] private ParticleSystem jetpack;
    private Vocal jetpackSFX;

    private Camera _camera;
    
    private Vector3 dir;
    
    Vector3 toolDirection;
    
    private bool isPaused;

    private void Awake()
    {
        inputReader.MoveEvent += HandleMove;
        inputReader.SecondaryFireEvent += HandleSecondaryFire;
        inputReader.AimEvent += HandleAim;
        inputReader.PauseEvent += HandlePause;

        _rb = GetComponent<Rigidbody>();

    }

    private void HandlePause(bool pause)
    {
	    isPaused = !isPaused;
	    Time.timeScale = isPaused ? 0f : 1f;
    }

    private void Start()
    {
        _camera = Camera.main;
        grappling.ClearLine();
		grapplingHookSFX = grappling.GetComponent<Vocal>();
        jetpackSFX = grappling.GetComponent<Vocal>();
        if (handController.GetComponent<LineRenderer>() != null)
        {

            handController.GetComponent<LineRenderer>().startWidth = 0.1f;
            handController.GetComponent<LineRenderer>().endWidth = 0.1f;
            handController.GetComponent<LineRenderer>().startColor = Color.white;
            handController.GetComponent<LineRenderer>().endColor = Color.white;
            
        }
        
	}

    #region Handle Input Events

    private void HandleMove(Vector2 moveVector)
    {

        currentMovementInput = moveVector;
        if (moveVector != Vector2.zero)
        {

            jetpackSFX.PlaySound();
            jetpack.Play();
        }
        else
        {
            jetpackSFX.StopSound();
            jetpack.Stop();
        }
    }


    private void HandleSecondaryFire(bool shoot)
    {
        if (shoot && !isGrappleActive)
        {
            // Unparent the hook while it is shooting
            //Move all to Grappling

            Vector3 direction = (aimIndicator.transform.position - this.transform.position).normalized;
            grappling.ShootHookRayCast(direction);
        }
        if (!shoot) 
        {
			grappling.StopGrappling();
			isGrappleActive = false;
		}
        
    }

    private void HandleAim(Vector2 mouseAimPosition)
    {
        previousAimInput = mouseAimPosition;
    }

    #endregion

    void Update()
    {

        Vector3 screenPosition = new(previousAimInput.x, previousAimInput.y, _camera.nearClipPlane);
        Vector3 worldPosition = _camera.ScreenToWorldPoint(screenPosition);
        aimIndicator.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f);
		handController.AimHand(aimIndicator.transform.position);
        if (handController.GetComponent<LineRenderer>() != null)
        {
            handController.GetComponent<LineRenderer>().SetPosition(0,handController.transform.position);
            handController.GetComponent<LineRenderer>().SetPosition(1, this.transform.position);
        }


    }

    private void FixedUpdate()
    {

    
    
	    _rb.AddForce(currentMovementInput * playerspeed, ForceMode.Force);
		
	}
    public void PullToHook(GameObject hook)
    {
        var grappleDir = hook.transform.position - transform.position;
        _rb.AddForce(grappleDir * grappling.grappleHookPull, ForceMode.Impulse);
        grapplingHookSFX.PlaySound();
    }

	private void OnDestroy()
    {        
		inputReader.SecondaryFireEvent -= HandleSecondaryFire;
		inputReader.AimEvent -= HandleAim;

        inputReader.MoveEvent -= HandleMove;
    }
}