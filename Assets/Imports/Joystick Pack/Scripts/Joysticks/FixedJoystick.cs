using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;
using UnityEngine.Windows;

public class FixedJoystick : Joystick
{
    public void HandleInputExternally(Vector2 simulatedInput)
    {
        input = simulatedInput;

        Vector2 radius = background.sizeDelta / 2;
        handle.anchoredPosition = input * radius * handleRange;
    }

}