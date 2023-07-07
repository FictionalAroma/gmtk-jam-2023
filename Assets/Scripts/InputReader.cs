using System;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using static Input.Controls;

[CreateAssetMenu(fileName = "New Input Reader", menuName = "Input/Input Reader", order = 0)]
public class InputReader : ScriptableObject, IPlayerActions
{
    public event Action<bool> PrimaryFireEvent; 
    public event Action<Vector2> MoveEvent;
    public event Action<bool> SecondaryFireEvent;
    public event Action<Vector2> AimEvent;
    
    private Controls controls;
    
    private void OnEnable()
    {
        if (controls == null)
        {
            controls = new Controls();
            controls.Player.SetCallbacks(this);
        }

        controls.Player.Enable();
    }
    
    public void OnMove(InputAction.CallbackContext context)
    {
        MoveEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.performed)
            PrimaryFireEvent?.Invoke(true);
        else if (context.canceled)
            PrimaryFireEvent?.Invoke(false);
    }

    public void OnSecondaryFire(InputAction.CallbackContext context)
    {
        if (context.performed)
        SecondaryFireEvent?.Invoke(true);
        
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        AimEvent.Invoke(context.ReadValue<Vector2>());
    }
}
