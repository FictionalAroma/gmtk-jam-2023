using UnityEngine;



public class Hazards : MonoBehaviour
{
    public enum HazardType{
        fire,
        metalCrack
    }
    [SerializeField] int hazardMaxHp;
    [SerializeField] int hazardCurrentHp;
    ShipController shipController;
    [SerializeField] int hazardDamage;
    [SerializeField] float hazardCooldown;
    float hazardCooldownTimer;
    [SerializeField]GameObject[] fires;
    [SerializeField] HazardType hazardType;
    [SerializeField] GameObject metalCrack;
    public bool hazardIsActive;
    private Vocal hazardSFX;
    [SerializeField]GameObject fireModel;
    [SerializeField] bool test;
    // Start is called before the first frame update

    void Start()
    {
        hazardCooldownTimer = hazardCooldown;
        hazardCurrentHp = hazardMaxHp;
        hazardIsActive = false;
        shipController = FindObjectOfType<ShipController>();
        hazardSFX = GetComponent<Vocal>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hazardIsActive)
        {
            hazardCooldownTimer -= Time.deltaTime;
            if (hazardCurrentHp <= 0)
            {

                DeactivateHazard();
            }
            if (hazardCooldownTimer <= 0)
            {
                if (shipController != null)
                {
                    shipController.TakeDamage(hazardDamage);
                    hazardCooldownTimer = hazardCooldown;
                }
                
            }
        }
        if (test)
        {
            Test();
        }
        
    }
    public void Test()
    {
        
        ActivateHazard();
    }
    private void DeactivateHazard()
    {
        if(hazardType == HazardType.fire)
        {
            fireModel.SetActive(false);
            this.GetComponent<Collider>().enabled = false;
            hazardIsActive = false;
        }
        if (hazardType == HazardType.metalCrack)
        {
            metalCrack.SetActive(false);
            this.GetComponent<Collider>().enabled=false;
            hazardIsActive = false;
        }
        hazardSFX.StopSound();
    }

    public void ActivateHazard()
    {
        if (hazardType == HazardType.fire)
        {
            fireModel = fires[Random.Range(0, fires.Length)];
            fireModel.SetActive(true);
         
            
        }
        else
        {
            metalCrack.SetActive(true);
            metalCrack.transform.parent = this.transform;
            
            
        }
        this.GetComponent<Collider>().enabled = true;
        hazardIsActive = true;
        hazardSFX.PlaySound();
    }
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other);
        if ((hazardType == HazardType.fire&&other.CompareTag("FireExtinguisher")) || (hazardType == HazardType.metalCrack&& other.CompareTag("Welder")))
        {
            hazardCurrentHp -= 1;
        }
    }
}
