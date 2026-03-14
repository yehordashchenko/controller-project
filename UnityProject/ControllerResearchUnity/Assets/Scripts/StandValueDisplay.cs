using UnityEngine;
using TMPro;

public class StandValueDisplay : MonoBehaviour
{
    public PlayerController player;
    public TargetStand stand;
    public TextMeshPro text;

    void Update()
    {
        if (player == null || stand == null) return;

        switch (stand.standType)
        {
            case TargetStand.StandType.VibrationStrengthPlus:
            case TargetStand.StandType.VibrationStrengthMinus:

                text.text =
                "VIBRATION\n" +
                player.vibrationLow.ToString("F2");
                break;


            case TargetStand.StandType.VibrationDurationPlus:
            case TargetStand.StandType.VibrationDurationMinus:

                text.text =
                "DURATION\n" +
                player.vibrationDuration.ToString("F2");
                break;


            case TargetStand.StandType.DeadZoneIncrease:
            case TargetStand.StandType.DeadZoneDecrease:

                text.text =
                "DEAD ZONE\n" +
                player.deadZone.ToString("F2");
                break;


            case TargetStand.StandType.DeadZoneAxial:
                text.text = "SET\nAXIAL";
                break;

            case TargetStand.StandType.DeadZoneRadial:
                text.text = "SET\nRADIAL";
                break;

            case TargetStand.StandType.DeadZoneScaledRadial:
                text.text = "SET\nSCALED";
                break;
        }
    }
}
