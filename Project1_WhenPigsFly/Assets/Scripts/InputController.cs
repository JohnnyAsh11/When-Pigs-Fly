using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private MovementController movementController;

    [SerializeField]
    private ProjectileController projectileController;

    public void OnMove(InputAction.CallbackContext context)
    {
        movementController.SetDirection(context.ReadValue<Vector2>());
    }

    public void OnFire()
    {
        projectileController.IsFiring = true;
    }
}
