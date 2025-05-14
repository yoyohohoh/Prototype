using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardControl : MonoBehaviour
{
    public FixedJoystick joystick;
    private InputSystem_Actions _inputActions;
    private Vector2 _moveInput;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _inputActions.Player.Move.performed += ctx => _moveInput = ctx.ReadValue<Vector2>();
        _inputActions.Player.Move.canceled += ctx => _moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        _inputActions.Player.Move.performed -= ctx => _moveInput = ctx.ReadValue<Vector2>();
        _inputActions.Player.Move.canceled -= ctx => _moveInput = Vector2.zero;
        _inputActions.Disable();
    }

    private void Update()
    {
        SimulateJoystickInput(_moveInput);
    }

    void SimulateJoystickInput(Vector2 input)
    {
        input = Vector2.ClampMagnitude(input, 1f);
        joystick.HandleInputExternally(input);
    }
}
