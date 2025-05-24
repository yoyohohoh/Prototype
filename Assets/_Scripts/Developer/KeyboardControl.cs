// -----------------------------------------------------------------------------
// Created by: yobisaboy
// This code is original and owned by yobisaboy. 
// Use requires logo inclusion and credit in-game and on publishing platforms.
// Redistribution or modification must include proper attribution.
// Contact: yobisaboy@gmail.com
// -----------------------------------------------------------------------------

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
        _inputActions.Player.Attack.performed += ctx => SimulateButtonInput(PlayerController.Instance._controlPanel.transform.Find("Attack").GetComponent<ButtonInteraction>(), true); // H
        _inputActions.Player.Attack.canceled += ctx => SimulateButtonInput(PlayerController.Instance._controlPanel.transform.Find("Attack").GetComponent<ButtonInteraction>(), false); // H

        _inputActions.Player.Jump.performed += ctx => SimulateButtonInput(PlayerController.Instance._controlPanel.transform.Find("Jump").GetComponent<ButtonInteraction>(), true); // J
        _inputActions.Player.Jump.canceled += ctx => SimulateButtonInput(PlayerController.Instance._controlPanel.transform.Find("Jump").GetComponent<ButtonInteraction>(), false); // J

        _inputActions.Player.Dash.performed += ctx => SimulateButtonInput(PlayerController.Instance._controlPanel.transform.Find("Dash").GetComponent<ButtonInteraction>(), true); // N
        _inputActions.Player.Dash.canceled += ctx => SimulateButtonInput(PlayerController.Instance._controlPanel.transform.Find("Dash").GetComponent<ButtonInteraction>(), false); // N

        _inputActions.Player.Skill.performed += ctx => SimulateButtonInput(PlayerController.Instance._controlPanel.transform.Find("Skill").GetComponent<ButtonInteraction>(), true); // B
        _inputActions.Player.Skill.canceled += ctx => SimulateButtonInput(PlayerController.Instance._controlPanel.transform.Find("Skill").GetComponent<ButtonInteraction>(), false); // B
    }

    void SimulateJoystickInput(Vector2 input)
    {
        input = Vector2.ClampMagnitude(input, 1f);
        joystick.HandleInputExternally(input);
    }

    void SimulateButtonInput(ButtonInteraction button, bool isPressed)
    {
        button.isPressed = isPressed;
    }
}
