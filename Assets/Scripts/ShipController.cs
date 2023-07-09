using System.Collections;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    SkyboxController skyboxController;
    public float dodgePower;
    [SerializeField] GameObject dirobject;
    //[SerializeField] BoxCollider[] colliders;
    [Tooltip("how far the ship moves on x axis")][SerializeField] float xRange = 5f;
    [Tooltip("how far the ship moves on y axis")][SerializeField] float yRange = 3.5f;
    Vector2 dodgeTopRight = new Vector2 (-1, -1);
    Vector2 dodgeTopLeft = new Vector2 (1, -1);
    Vector2 dodgeBotRight = new Vector2 (-1, 1);
    Vector2 dodgeBotLeft = new Vector2 (1, 1);
    [SerializeField] float positionPitchFactor = 2f;
    [SerializeField] float positionRollFactor = -5f;
    [SerializeField] float ControlPitchFactor = -10f;
    [SerializeField] float controlYawFactor = 5f;
    [SerializeField] float controlRollFactor = 20f;
    [SerializeField] bool isDodging;
    Vector2 direction;
    float yThrow;
    float xThrow;
    [SerializeField] int hullMaxHp;
    [SerializeField] int hullCurrentHp;
    AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindObjectOfType(typeof(AudioManager)) as AudioManager;
        hullCurrentHp = hullMaxHp;
        skyboxController = FindObjectOfType<SkyboxController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDodging)
        {
            if (direction.y > 0 && direction.x > 0)
            {
                ProcessTranslation(dodgeTopRight * dodgePower);

            }
            if (direction.y > 0 && direction.x < 0)
            {
                ProcessTranslation(dodgeTopLeft * dodgePower);
            }
            if (direction.y < 0 && direction.x > 0)
            {
                ProcessTranslation(dodgeBotRight * dodgePower);
            }
            if (direction.y < 0 && direction.x < 0)
            {
                ProcessTranslation(dodgeBotLeft * dodgePower);
            }
            Debug.Log(direction);
            Debug.Log(xThrow);
            
        }
        else
        {
            ProcessTranslation(Vector2.zero);
            
            
        }
        ProcessRotation();

    }

    void ProcessTranslation(Vector2 vector2Throw)
    {
        Debug.Log($"Vector2 Throw: {vector2Throw}");
        xThrow = vector2Throw.x;
        yThrow = vector2Throw.y;

        float xOffset = xThrow * Time.deltaTime * dodgePower;
        float yOffset = yThrow * Time.deltaTime * dodgePower;
        float rawXPos = transform.position.x + xOffset;
        float rawYPos = transform.position.y + yOffset;
        float clampedXpos = Mathf.Clamp(rawXPos, -xRange, xRange);
        float clampedYpos = Mathf.Clamp(rawYPos, -yRange, yRange);
        transform.position = new Vector3(clampedXpos, clampedYpos, transform.position.z);
    }

    void ProcessRotation()
    {
        float pitch = transform.position.y * positionPitchFactor*System.Convert.ToInt32(isDodging) + yThrow * ControlPitchFactor*Time.deltaTime;
        float yaw = transform.position.x * controlYawFactor* System.Convert.ToInt32(isDodging);
        float roll = transform.position.x * positionRollFactor* System.Convert.ToInt32(isDodging) + xThrow;
        Debug.Log($"Pitch :{pitch}, Yaw : {yaw}, Roll: {roll}");
        transform.rotation = Quaternion.Euler(pitch, yaw, roll);
    }


    //returns -1 when to the left, 1 to the right, and 0 for forward/backward
    /*public float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up)
    {
        Vector3 perp = Vector3.Cross(fwd, targetDir);
        float dir = Vector3.Dot(perp, up);

        if (dir > 0.0f)
        {
            return 1.0f;
        }
        else if (dir < 0.0f)
        {
            return -1.0f;
        }
        else
        {
            return 0.0f;
        }
    }*/
    void DetectThreat(GameObject threat)
    {
        if (threat.gameObject.tag == "Asteroid")
        {
           
            StartCoroutine(Dodge());
        }
        if (threat.gameObject.tag == "EnemySpaceShip")
        {
            /*var dir = (threat.transform.position - this.transform.position).normalized;
            Debug.Log(dir);
            Instantiate(dirobject, -dir*10, Quaternion.identity);
            StartCoroutine(Dodge(-dir));*/
            StartCoroutine(Dodge());
        }
          
    }

    void Turn(float rotation)
    {

    }
    IEnumerator Dodge()
    {
        isDodging = true;
        yield return new WaitForSeconds(2);
        isDodging = false;
    }
    void Fire()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
       StartCoroutine (Dodge());
       direction = (other.gameObject.transform.position - transform.position).normalized;
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
    }
    public void TakeDamage(int damage)
    {
        hullCurrentHp = -damage;
    }
}
