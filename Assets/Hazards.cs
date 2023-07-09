using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Hazards : MonoBehaviour
{
    public enum HazardType{
        fire,
        metalCrack
    }
    [SerializeField] int hazardHp;
    ShipController shipController;
    [SerializeField] int hazardDamage;
    [SerializeField] float hazardCooldown;
    float hazardCooldownTimer;
    [SerializeField]GameObject[] fires;
    [SerializeField] HazardType hazardType;
    [SerializeField] GameObject metalCrack;
    [SerializeField] bool hazardIsActive;
    GameObject crackModel;
    GameObject fireModel;
    // Start is called before the first frame update
    void Start()
    {
        hazardCooldownTimer = hazardCooldown;
        
        shipController = FindObjectOfType<ShipController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hazardIsActive)
        {
            hazardCooldownTimer -= Time.deltaTime;
            if (hazardHp <= 0)
            {
                hazardIsActive = false;
                
            }
            if (hazardCooldownTimer <= 0)
            {
                shipController.TakeDamage(hazardDamage);
                hazardCooldownTimer = hazardCooldown;
            }
        }
        
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
            crackModel.SetActive(true);
            crackModel.transform.parent = this.transform;
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        if ((hazardType == HazardType.fire&&other.CompareTag("FireExtinguisher")) || (hazardType == HazardType.metalCrack&& other.CompareTag("Welder")))
        {
            hazardHp -= 1;
        }
    }
}
