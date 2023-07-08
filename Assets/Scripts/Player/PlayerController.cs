using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;

    [SerializeField] private float playerspeed = 10f;
    private Vector2 currentMovementInput;
    [SerializeField] GrapplingHook grappleHookHead;
    private Camera _camera;
    private Vector2 previousAimInput;
    [SerializeField] GameObject aimIndicator;
    Vector3 dir;
	private Rigidbody _rb;

	private void Awake()
    {
        inputReader.MoveEvent += HandleMove;
        inputReader.PrimaryFireEvent += HandlePrimaryFire;
        inputReader.SecondaryFireEvent += HandleSecondaryFire;
        inputReader.AimEvent += HandleAim;

		_rb = GetComponent<Rigidbody>();

	}
    private void Start()
    {
        _camera = Camera.main;
    }



    void Update()
    {
        var aimPoint = _camera.ScreenToWorldPoint(previousAimInput);
        dir = this.transform.position -(Vector3)previousAimInput;
        dir = -dir;
        aimIndicator.transform.position = dir;
        Debug.Log("aim dir : " + dir);

    }

	private void FixedUpdate()
	{
		_rb.AddForce(currentMovementInput * (Time.fixedDeltaTime * playerspeed), ForceMode.Impulse);
	}

	public void MoveToGrapple(Vector3 grapplePosition, float grappleHookPull)
    {
        Debug.Log("Pulling to Hook");
        var dir = grapplePosition - this.transform.position;
		_rb.AddForce(dir*grappleHookPull,ForceMode.Impulse);
    }
    private void HandleMove(Vector2 moveVector)
    {
        Debug.Log(moveVector);
        currentMovementInput = moveVector;
    }
    
    private void HandlePrimaryFire(bool shoot)
    {
        if (shoot)
            Debug.Log("Bang");
    }
    private void HandleSecondaryFire(bool shoot)
    {
        if(shoot)
        {
            var grapplingHook = Instantiate<GrapplingHook>(grappleHookHead,transform.position, Quaternion.identity);
			grapplingHook.Init(this);
            grapplingHook.transform.forward = dir - grapplingHook.transform.position;
            Debug.Log(grapplingHook.transform.rotation);
        }
    }

    private void HandleAim(Vector2 mouseAimPosition)
    {
        previousAimInput = mouseAimPosition;
        Debug.Log("mouse aim Pos:"+mouseAimPosition);
    }
    private void OnDestroy()
    {
        inputReader.MoveEvent -= HandleMove;
        inputReader.PrimaryFireEvent -= HandlePrimaryFire;
    }
}
