using System;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;

    private void Awake()
    {
        inputReader.MoveEvent += HandleMove;
        inputReader.PrimaryFireEvent += HandlePrimaryFire;
    }
    
    private void HandleMove(Vector2 moveVector)
    {
        Debug.Log(moveVector);
    }
    
    private void HandlePrimaryFire(bool shoot)
    {
        if (shoot)
            Debug.Log("Bang");
    }

    private void OnDestroy()
    {
        inputReader.MoveEvent -= HandleMove;
        inputReader.PrimaryFireEvent -= HandlePrimaryFire;
    }
}
