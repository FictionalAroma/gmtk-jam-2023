using System.Collections;
using UnityEditor;
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

    public bool isGrappleActive;
    private Camera _camera;
    private Vector2 previousAimInput;
    private Vector2 currentMovementInput;
    private Vector3 dir;
    private Rigidbody _rb;
    [SerializeField] GameObject currentTool;
    Vector3 toolDirection;

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
        grappleHookHead.ClearLine();
    }

    #region Handle Input Events

    private void HandleMove(Vector2 moveVector)
    {
        currentMovementInput = moveVector;
    }

    private void HandlePrimaryFire(bool shoot)
    {
        if (shoot)
        {
            
            currentTool.GetComponent<Tools>().UseTool();
        }
        else
        {
            currentTool.GetComponent<Tools>().StopUseTool();
        }
    }

    private void HandleSecondaryFire(bool shoot)
    {
        if (shoot && !isGrappleActive)
        {
            // Unparent the hook while it is shooting
            grappleHookHead.transform.parent = null;

            StopGrappling();
            Vector3 direction = (aimIndicator.transform.position - grappleHookHead.transform.position).normalized;
            grappleHookHead.GetComponent<Rigidbody>().AddForce(direction * grappleHookHead.grappleHookPower, ForceMode.Impulse);

            isGrappleActive = true;
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

        Vector3 directionToTarget = aimIndicator.transform.position - grappleHookHead.transform.position;
        
        
        float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
        grappleHookHead.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (currentTool != null)
        {
            toolDirection = aimIndicator.transform.position - currentTool.transform.position;
            currentTool.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
           
       
    }

    private void FixedUpdate()
    {
        _rb.AddForce(currentMovementInput * (Time.fixedDeltaTime * playerspeed), ForceMode.Impulse);
    }

    public void MoveToGrapple(Vector3 grapplePosition, float grappleHookPull)
    {
        Debug.Log("Pulling to Hook");
        var dir = grapplePosition - transform.position;
        _rb.AddForce(dir * grappleHookPull, ForceMode.Impulse);
        StartCoroutine(RetractGrapple());
    }

    private IEnumerator RetractGrapple()
    {
        yield return new WaitForSeconds(grappleRetractionDelay);

        while (Vector3.Distance(grappleHookHead.transform.position, transform.position) > 0.1f)
        {
            grappleHookHead.transform.position = Vector3.MoveTowards(grappleHookHead.transform.position, transform.position, grappleRetractionSpeed * Time.deltaTime);
            yield return null;
        }

        // Re-parent the hook when it's done retracting
        grappleHookHead.transform.parent = transform;
    }


    public void StopGrappling()
    {
        // Reset the grapple
        grappleHookHead.GetComponent<Rigidbody>().velocity = Vector3.zero;
        grappleHookHead.transform.position = transform.position;

        // Re-parent the hook immediately if it's not in use
        grappleHookHead.transform.parent = transform;

        // Remove the line
        grappleHookHead.ClearLine();
    }


    private void OnDestroy()
    {
        inputReader.MoveEvent -= HandleMove;
        inputReader.PrimaryFireEvent -= HandlePrimaryFire;
    }
}