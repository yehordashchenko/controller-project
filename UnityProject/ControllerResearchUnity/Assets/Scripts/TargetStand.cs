using UnityEngine;

public class TargetStand : MonoBehaviour
{
    public enum StandType
    {
        DeadZoneAxial,
        DeadZoneRadial,
        DeadZoneScaledRadial,

        DeadZoneIncrease,
        DeadZoneDecrease,

        VibrationStrengthPlus,
        VibrationStrengthMinus,

        VibrationDurationPlus,
        VibrationDurationMinus
    }

    public StandType standType;

    public PlayerController player;

    public AudioSource hitSound;

    public float deadZoneStep = 0.02f;
    public float vibrationStep = 0.05f;
    public float durationStep = 0.02f;

    public void Activate()
    {
        if (hitSound != null)
            hitSound.Play();

        switch (standType)
        {
            case StandType.DeadZoneAxial:
                player.deadZoneType = PlayerController.DeadZoneType.Axial;
                break;

            case StandType.DeadZoneRadial:
                player.deadZoneType = PlayerController.DeadZoneType.Radial;
                break;

            case StandType.DeadZoneScaledRadial:
                player.deadZoneType = PlayerController.DeadZoneType.ScaledRadial;
                break;

            case StandType.DeadZoneIncrease:
                player.deadZone += deadZoneStep;
                player.deadZone = Mathf.Clamp(player.deadZone, 0f, 0.5f);
                break;

            case StandType.DeadZoneDecrease:
                player.deadZone -= deadZoneStep;
                player.deadZone = Mathf.Clamp(player.deadZone, 0f, 0.5f);
                break;

            case StandType.VibrationStrengthPlus:
                player.vibrationLow += vibrationStep;
                player.vibrationHigh += vibrationStep;
                break;

            case StandType.VibrationStrengthMinus:
                player.vibrationLow -= vibrationStep;
                player.vibrationHigh -= vibrationStep;
                break;

            case StandType.VibrationDurationPlus:
                player.vibrationDuration += durationStep;
                break;

            case StandType.VibrationDurationMinus:
                player.vibrationDuration -= durationStep;
                break;
        }
    }
}
