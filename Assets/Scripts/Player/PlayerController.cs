using System.Collections;
using Assets.Scripts.Player;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;

    [SerializeField] private float playerspeed = 10f;
    [SerializeField] private GrapplingHook grappleHookHead;
    [SerializeField] GameObject aimIndicator;
    [SerializeField] private float grappleRetractionSpeed = 10f;
    [SerializeField] private float grappleRetractionDelay = .2f;
	[SerializeField] private PlayerPickupController handController;
	
    public bool isGrappleActive;
    private Camera _camera;
    private Vector2 previousAimInput;
    private Vector2 currentMovementInput;
    private Vector3 dir;
    private Rigidbody _rb;
    Vector3 toolDirection;
    AudioManager audioManager;
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
        grappleHookHead.ClearLine();
        audioManager = FindObjectOfType(typeof(AudioManager)) as AudioManager;
    }

    #region Handle Input Events

    private void HandleMove(Vector2 moveVector)
    {
        currentMovementInput = moveVector;
    }


    private void HandleSecondaryFire(bool shoot)
    {
        if (shoot && !isGrappleActive)
        {
            // Unparent the hook while it is shooting
            grappleHookHead.transform.parent = this.transform.parent;
            Vector3 direction = (aimIndicator.transform.position - grappleHookHead.transform.position).normalized;
			float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            grappleHookHead.Shoot(direction, angle);

            isGrappleActive = true;
        }
        if (!shoot) 
        {
			grappleHookHead.StopGrappling();
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

        Vector3 screenPosition = new Vector3(previousAimInput.x, previousAimInput.y, _camera.nearClipPlane);
        Vector3 worldPosition = _camera.ScreenToWorldPoint(screenPosition);
        aimIndicator.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f);
		handController.AimHand(aimIndicator.transform.position);
		
	}

    private void FixedUpdate()
    {
		if (grappleHookHead.IsConnected)
		{
			var grappleDir = grappleHookHead.transform.position - transform.position;
			_rb.AddForce(grappleDir * grappleHookHead.grappleHookPull, ForceMode.Force);
            audioManager.PlayPlayerSFX(audioManager.playerAudioClips[AudioManager.PlayerAudioClips.grapplinghookSFX]);

		}
		else
		{
			_rb.AddForce(currentMovementInput * playerspeed, ForceMode.Force);
		}
	}

	private void OnDestroy()
    {        
		inputReader.SecondaryFireEvent -= HandleSecondaryFire;
		inputReader.AimEvent -= HandleAim;

        inputReader.MoveEvent -= HandleMove;
    }
}