using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;

    [SerializeField] private float playerspeed = 10f;
    private Vector2 previousMovementInput;
    [SerializeField] GameObject grappleHookHead;
    private Camera _camera;
    private Vector2 previousAimInput;
    [SerializeField] GameObject aimIndicator;
    Vector3 dir;

    private void Awake()
    {
        inputReader.MoveEvent += HandleMove;
        inputReader.PrimaryFireEvent += HandlePrimaryFire;
        inputReader.SecondaryFireEvent += HandleSecondaryFire;
        inputReader.AimEvent += HandleAim;
        
    }
    private void Start()
    {
        _camera = Camera.main;
    }



    void Update()
    {
        this.gameObject.transform.Translate(previousMovementInput*Time.deltaTime*playerspeed);
        var aimPoint = _camera.ScreenToWorldPoint(previousAimInput);
        dir = this.transform.position -(Vector3)previousAimInput;
        dir = -dir;
        aimIndicator.transform.position = dir;
        Debug.Log("aim dir : " + dir);

    }
    public void MoveToGrapple(Vector3 grapplePosition, float grappleHookPull)
    {
        Debug.Log("Pulling to Hook");
        var dir = grapplePosition - this.transform.position;
        this.GetComponent<Rigidbody>().AddForce(dir*grappleHookPull,ForceMode.Impulse);
    }
    private void HandleMove(Vector2 moveVector)
    {
        Debug.Log(moveVector);
        previousMovementInput = moveVector;
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
            var grapplingHook = Instantiate(grappleHookHead,transform.position, Quaternion.identity);
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
