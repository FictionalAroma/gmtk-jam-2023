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
    
    private void Awake()
    {
        inputReader.MoveEvent += HandleMove;
        inputReader.PrimaryFireEvent += HandlePrimaryFire;
        inputReader.SecondaryFireEvent += HandleSecondaryFire;
    }



    void Update()
    {
        this.gameObject.transform.Translate(previousMovementInput*Time.deltaTime*playerspeed);

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
            grapplingHook.transform.forward = HandleAim();
        }
    }

    private Vector2 HandleAim()
    {
        Vector2 aim = UnityEngine.Input.mousePosition;
        return aim;
    }
    private void OnDestroy()
    {
        inputReader.MoveEvent -= HandleMove;
        inputReader.PrimaryFireEvent -= HandlePrimaryFire;
    }
}
