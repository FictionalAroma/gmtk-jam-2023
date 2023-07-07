using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class InputTest : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;

    [SerializeField] private float playerspeed = 10f;
    private Vector2 previousMovementInput;

    private void Awake()
    {
        inputReader.MoveEvent += HandleMove;
        inputReader.PrimaryFireEvent += HandlePrimaryFire;
    }

    void Update()
    {
        this.gameObject.transform.Translate(previousMovementInput*Time.deltaTime*playerspeed);

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
