using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadDebug : MonoBehaviour
{
    void Update()
    {
        if (Gamepad.current == null) return;

        var g = Gamepad.current;

        Debug.Log($"Name: {g.name}");
        Debug.Log($"Display Name: {g.displayName}");
        Debug.Log($"Layout: {g.layout}");
    }
}