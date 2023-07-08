using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] int fireHp;
    ShipController shipController;
    [SerializeField] int fireDamage;
    [SerializeField] float fireCooldown;
    float fireCooldownTimer;
    [SerializeField]GameObject[] fires;
    // Start is called before the first frame update
    void Start()
    {
        fireCooldownTimer = fireCooldown;
        Instantiate(fires[Random.Range(0, fires.Length)], this.transform.position, Quaternion.identity);
        shipController= FindObjectOfType<ShipController>();
    }

    // Update is called once per frame
    void Update()
    {
        fireCooldownTimer -= Time.deltaTime;
        if (fireHp<=0)
        {
            Destroy(this.gameObject);
        }
        if (fireCooldownTimer<=0)
        {
            shipController.TakeDamage(fireDamage);
            fireCooldownTimer = fireCooldown;
        }
    }
    
    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "FireExtinguisher")
        {
            fireHp -= 1;
        }
    }
}
