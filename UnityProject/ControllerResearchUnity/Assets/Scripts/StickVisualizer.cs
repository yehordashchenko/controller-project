using UnityEngine;
using UnityEngine.UI;

public class StickVisualizer : MonoBehaviour
{
    public RectTransform rawPoint;
    public RectTransform processedPoint;

    public RectTransform deadZoneArea;
    public Image deadZoneImage;

    public Sprite circleSprite;
    public Sprite squareSprite;

    public float radius = 100f;

    Vector2 rawInput;
    Vector2 processedInput;

    public void SetInput(Vector2 raw, Vector2 processed)
    {
        rawInput = raw;
        processedInput = processed;
    }

    public void UpdateDeadZone(float deadZone, PlayerController.DeadZoneType type)
    {
        float size = radius * deadZone * 2f;

        deadZoneArea.sizeDelta = new Vector2(size, size);

        if(type == PlayerController.DeadZoneType.Axial)
            deadZoneImage.sprite = squareSprite;
        else
            deadZoneImage.sprite = circleSprite;
    }

    void Update()
    {
        rawPoint.anchoredPosition = rawInput * radius;
        processedPoint.anchoredPosition = processedInput * radius;
    }
}