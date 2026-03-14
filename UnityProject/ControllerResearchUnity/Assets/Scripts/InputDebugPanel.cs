using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class InputDebugPanel : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    public PlayerController player;

    Vector2 rawInput;
    Vector2 processedInput;

    string currentDevice = "None";

    public void SetInput(Vector2 raw, Vector2 processed)
    {
        rawInput = raw;
        processedInput = processed;
    }

    void OnEnable()
    {
        InputSystem.onAnyButtonPress.Call(OnButtonPressed);
    }

    void OnDisable()
    {
        InputSystem.onAnyButtonPress.Remove(OnButtonPressed);
    }

    void OnButtonPressed(InputControl control)
    {
        var device = control.device;

        if (device is Gamepad)
        {
            string name = device.displayName;

            if (name.Contains("DualSense"))
                currentDevice = "DualSense Controller";

            else if (name.Contains("DualShock"))
                currentDevice = "DualShock Controller";

            else if (name.Contains("Xbox"))
                currentDevice = "Xbox Controller";

            else
                currentDevice = "Gamepad";
        }

        else if (device is Keyboard || device is Mouse)
        {
            currentDevice = "Keyboard & Mouse";
        }
    }

    void Update()
    {
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
            "Duration: " + player.vibrationDuration.ToString("F2");
    }
}
