using System;
using Cinemachine.Utility;
using UnityEngine;
using UnityEngine.InputSystem;
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
        
        Vector3 screenPosition = new Vector3(previousAimInput.x, previousAimInput.y, _camera.nearClipPlane);
        Vector3 worldPosition = _camera.ScreenToWorldPoint(screenPosition);
        aimIndicator.transform.position = new Vector3(worldPosition.x, worldPosition.y, 0f);
        
        Vector3 directionToTarget = aimIndicator.transform.position - grappleHookHead.transform.position;
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        grappleHookHead.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
            Vector3 direction = (aimIndicator.transform.position - grappleHookHead.transform.position).normalized;
            grappleHookHead.GetComponent<Rigidbody>().AddForce(direction * grappleHookHead.grappleHookPower, ForceMode.Impulse);
            Debug.Log(grappleHookHead.transform.rotation);
        }
    }

    private void HandleAim(Vector2 mouseAimPosition)
    {
        previousAimInput = mouseAimPosition;
    }
    private void OnDestroy()
    {
        inputReader.MoveEvent -= HandleMove;
        inputReader.PrimaryFireEvent -= HandlePrimaryFire;
    }
}
