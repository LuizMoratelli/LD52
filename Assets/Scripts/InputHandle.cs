using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandle : MonoBehaviour
{
    public Vector2 MoveDirection { get; private set; }
    private Movement movementComponent;
    private PlayerInput playerInput;
    private UpgradeSystem upgradeSystemComponent;

    private void Awake()
    {
        movementComponent = gameObject.GetComponent<Movement>();
        playerInput = gameObject.GetComponent<PlayerInput>();
        upgradeSystemComponent = gameObject.GetComponent<UpgradeSystem>();
       setupActions();

    }

    public void Move(InputAction.CallbackContext context)
    {
        MoveDirection = context.ReadValue<Vector2>();

    }

    public void LeftAction(InputAction.CallbackContext context)
    {
        upgradeSystemComponent.BuyTerrain();
    }

    public void MiddleAction(InputAction.CallbackContext context)
    {
        upgradeSystemComponent.BuyScythe();

    }

    public void RightAction(InputAction.CallbackContext context)
    {
        upgradeSystemComponent.BuySpeed();

    }

    public void TopAction(InputAction.CallbackContext context)
    {
        upgradeSystemComponent.BuyRecoverHealth();

    }

    void setupActions()
    {
        playerInput.actions["LeftAction"].performed += LeftAction;
        playerInput.actions["MiddleAction"].performed += MiddleAction;
        playerInput.actions["RightAction"].performed += RightAction;
        playerInput.actions["TopAction"].performed += TopAction;
    }

    void FixedUpdate()
    {
        movementComponent?.Translate(MoveDirection);
    }
}
