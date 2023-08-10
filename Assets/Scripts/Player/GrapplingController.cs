using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class GrapplingController : MonoBehaviour
{
    public GameObject[] hookHands;
    public float grappleHookPull;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetHookPosition(Vector3 direction)
    {
        RaycastHit hookHit;
        if (Physics.Raycast(this.transform.position, direction, out hookHit, Mathf.Infinity, LayerMask.GetMask("ShipWalls")))
        {
            var closestHand = ChooseClosestHand(hookHit.point);
            closestHand.GetComponent<GrappleHook>().ShootHook(hookHit.point);
        }
    }
    public GameObject ChooseClosestHand(Vector3 hookPoint)
    {
        GameObject closestHand = null;
        float closestDistance = float.MaxValue;
        foreach (var hookHand in hookHands)
        {
            float distance = Vector3.Distance(hookPoint,hookHand.gameObject.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestHand = hookHand;
            }
        }
        return closestHand;
        
    }
}
