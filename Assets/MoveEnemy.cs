using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found");
            Destroy(gameObject);
        }
        else
        {
            float spinX = UnityEngine.Random.Range(-5f, 5f);
            float spinY = UnityEngine.Random.Range(-5f, 5f);
            float spinZ = UnityEngine.Random.Range(-5f, 5f);

            rb.angularVelocity = new Vector3(spinX, spinY, spinZ);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.back * 100f, ForceMode.Force);
    }
}
