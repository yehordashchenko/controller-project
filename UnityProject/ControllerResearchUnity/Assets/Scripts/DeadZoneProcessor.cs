using UnityEngine;

public static class DeadZoneProcessor
{
    public static Vector2 ApplyAxialDeadZone(Vector2 input, float deadZone)
    {
        float x = Mathf.Abs(input.x) < deadZone ? 0 : input.x;
        float y = Mathf.Abs(input.y) < deadZone ? 0 : input.y;

        return new Vector2(x, y);
    }

    public static Vector2 ApplyRadialDeadZone(Vector2 input, float deadZone)
    {
        if (input.magnitude < deadZone)
            return Vector2.zero;

        return input;
    }

    public static Vector2 ApplyScaledRadialDeadZone(Vector2 input, float deadZone)
    {
        float magnitude = input.magnitude;

        if (magnitude < deadZone)
            return Vector2.zero;

        float scaledMagnitude = (magnitude - deadZone) / (1 - deadZone);
        return input.normalized * scaledMagnitude;
    }
}