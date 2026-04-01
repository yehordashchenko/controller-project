using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class InputDebugPanel : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    public PlayerController player;

    Gamepad activeGamepad;

    float lastGamepadTime;
    float lastKeyboardTime;
    float deviceSwitchCooldown = 0.5f;

    Vector2 rawInput;
    Vector2 processedInput;

    string currentDevice = "None";

    public void SetInput(Vector2 raw, Vector2 processed)
    {
        rawInput = raw;
        processedInput = processed;
    }

    public Gamepad GetActiveGamepad()
    {
        return activeGamepad;
    }

    void Update()
    {
        DetectActiveDevice();

        float rawMagnitude = rawInput.magnitude;
        float processedMagnitude = processedInput.magnitude;

        debugText.text =
            "INPUT DEBUG PANEL\n\n" +

            "DEVICE\n" +
            currentDevice + "\n\n" +

            "RAW INPUT\n" +
            "X: " + rawInput.x.ToString("F2") + "\n" +
            "Y: " + rawInput.y.ToString("F2") + "\n\n" +

            "PROCESSED INPUT\n" +
            "X: " + processedInput.x.ToString("F2") + "\n" +
            "Y: " + processedInput.y.ToString("F2") + "\n\n" +

            "RAW MAGNITUDE: " + rawMagnitude.ToString("F3") + "\n" +
            "PROCESSED MAGNITUDE: " + processedMagnitude.ToString("F3") + "\n\n" +

            "DEAD ZONE SETTINGS\n" +
            "Type: " + player.deadZoneType.ToString() + "\n" +
            "Size: " + player.deadZone.ToString("F2") + "\n\n" +

            "VIBRATION SETTINGS\n" +
            "Strength: " + player.vibrationLow.ToString("F2") + "\n" +
            "Duration: " + player.vibrationDuration.ToString("F2") +

            "\n\nLOOK SETTINGS\n" +
            "Sensitivity: " + player.lookSensitivity.ToString("F0");
    }

    void DetectActiveDevice()
    {
        float currentTime = Time.time;
        bool isAnyControllerActive = false;

        foreach (var gamepad in Gamepad.all)
        {
            if (gamepad == null) continue;

            bool isActive =
                gamepad.leftStick.ReadValue().magnitude > 0.2f ||
                gamepad.rightStick.ReadValue().magnitude > 0.2f ||
                gamepad.leftTrigger.ReadValue() > 0.1f ||
                gamepad.rightTrigger.ReadValue() > 0.1f ||
                gamepad.buttonSouth.isPressed;

            if (isActive)
            {
                lastGamepadTime = currentTime;
                activeGamepad = gamepad;
                SetGamepadName(gamepad);
                isAnyControllerActive = true;
            }
        }

        foreach (var joystick in Joystick.all)
        {
            if (joystick == null) continue;

            bool isActive = joystick.stick.ReadValue().magnitude > 0.2f;

            if (isActive)
            {
                lastGamepadTime = currentTime;
                activeGamepad = null;
                currentDevice = $"DirectInput: {joystick.displayName}";
                isAnyControllerActive = true;
            }
        }

        if ((Keyboard.current != null && Keyboard.current.anyKey.isPressed) ||
            (Mouse.current != null && Mouse.current.delta.ReadValue().sqrMagnitude > 0.01f))
        {
            lastKeyboardTime = currentTime;
        }

        if (lastKeyboardTime > lastGamepadTime)
        {
            currentDevice = "Keyboard & Mouse";
            activeGamepad = null;
        }
    }

    void SetGamepadName(Gamepad gamepad)
    {
        if (gamepad == null) return;

        string layout = gamepad.layout.ToLower();
        string name = gamepad.displayName.ToLower();

        if (layout.Contains("dualsense"))
            currentDevice = "DualSense Controller";
        else if (layout.Contains("dualshock"))
            currentDevice = "DualShock Controller";
        else if (name.Contains("xbox"))
            currentDevice = "Xbox Controller";
        else
            currentDevice = "Generic XInput Gamepad";
    }
}
