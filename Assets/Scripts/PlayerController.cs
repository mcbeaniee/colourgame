using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;

    public float MovementSpeed = 7f, RotationSpeed = 10f, JumpForce=10f, Gravity = -30f, sensitivity = 10f;

    private float _rotationY;
    private float _rotationX;
    private float _verticalVelocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector2 movementVector)
    {
        Vector3 move = transform.forward * movementVector.y + transform.right * movementVector.x;
        move = move * MovementSpeed * Time.deltaTime;
        _characterController.Move(move);

        _verticalVelocity = _verticalVelocity + Gravity * Time.deltaTime;
        _characterController.Move(new Vector3(0, _verticalVelocity, 0) * Time.deltaTime);
    }

    public void Rotate(Vector2 rotationVector)
    {
        _rotationY += rotationVector.x * RotationSpeed * sensitivity *Time.deltaTime;
        _rotationX += rotationVector.y * RotationSpeed * sensitivity *Time.deltaTime;
        _rotationX = Mathf.Clamp(_rotationX, -90f, 90f);
        transform.localRotation = Quaternion.Euler(-_rotationX, _rotationY, 0f);
    }

    public void Jump()
    {
        if (_characterController.isGrounded)
        {
            _verticalVelocity = JumpForce;
        }
    }
}
