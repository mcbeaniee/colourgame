using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public PlayerController CharacterController;

    private InputAction _moveAction, _lookAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Move");
        _lookAction = InputSystem.actions.FindAction("Look");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movementVector = _moveAction.ReadValue<Vector2>();
        CharacterController.Move(movementVector);

        Vector2 lookVector = _lookAction.ReadValue<Vector2>();
        CharacterController.Rotate(lookVector);
    }
}
