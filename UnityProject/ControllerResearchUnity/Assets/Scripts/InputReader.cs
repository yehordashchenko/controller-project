using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public Vector2 Move;
    public Vector2 Look;

    public bool JumpPressed;
    public bool ShootPressed;

    public void OnMove(InputValue value)
    {
        Move = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        Look = value.Get<Vector2>();
        // Debug.Log($"RAW LOOK INPUT: {Look}");
    }

    public void OnJump()
    {
        JumpPressed = true;
    }

    public void OnShoot()
    {
        ShootPressed = true;
    }

    void LateUpdate()
    {
        JumpPressed = false;
        ShootPressed = false;
    }
}
