using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Transform cameraTransform;
    public float lookSensitivity = 100f;

    float xRotation = 0f;

    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    public float shootDistance = 50f;

    float coyoteTime = 0.15f;
    float coyoteTimeCounter;

    public StickVisualizer stickVisualizer;
    public InputDebugPanel debugPanel;

    public GameObject hitEffect;
    public AudioSource shootAudio;

    public float vibrationLow = 0.2f;
    public float vibrationHigh = 0.3f;
    public float vibrationDuration = 0.1f;

    public ParticleSystem muzzleFlash;

    public InputReader input;


    public enum DeadZoneType
    {
        Axial,
        Radial,
        ScaledRadial
    }

    public DeadZoneType deadZoneType = DeadZoneType.Radial;
    public float deadZone = 0.15f;

    Vector3 velocity;
    bool isGrounded;

    CharacterController controller;
    Coroutine vibrationRoutine;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    IEnumerator GamepadVibration(float low, float high, float duration)
    {
        var gamepad = debugPanel.GetActiveGamepad();

        if (gamepad != null)
        {
            gamepad.SetMotorSpeeds(low, high);

            yield return new WaitForSeconds(duration);

            gamepad.SetMotorSpeeds(0f, 0f);
        }
    }

    void Shoot()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        shootAudio.Play();

        if (muzzleFlash != null)
            muzzleFlash.Play();

        if (vibrationRoutine != null)
            StopCoroutine(vibrationRoutine);

        vibrationRoutine = StartCoroutine(GamepadVibration(vibrationLow, vibrationHigh, vibrationDuration));

        if (Physics.Raycast(ray, out hit, shootDistance))
        {
            GameObject effect = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(effect, 1f);

            TargetStand stand = hit.collider.GetComponent<TargetStand>();

            if (stand != null)
                stand.Activate();
        }
    }

    void Jump()
    {
        if (coyoteTimeCounter > 0f)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            coyoteTimeCounter = 0f;
        }
    }

    void Update()
    {
        Vector2 moveInput = input.Move;
        Vector2 lookInput = input.Look;

        Vector2 processedLook = lookInput;

        switch(deadZoneType)
        {
            case DeadZoneType.Axial:
                processedLook = DeadZoneProcessor.ApplyAxialDeadZone(lookInput, deadZone);
                break;

            case DeadZoneType.Radial:
                processedLook = DeadZoneProcessor.ApplyRadialDeadZone(lookInput, deadZone);
                break;

            case DeadZoneType.ScaledRadial:
                processedLook = DeadZoneProcessor.ApplyScaledRadialDeadZone(lookInput, deadZone);
                break;
        }

        stickVisualizer.SetInput(lookInput, processedLook);
        stickVisualizer.UpdateDeadZone(deadZone, deadZoneType);

        debugPanel.SetInput(lookInput, processedLook);

        float lookX = processedLook.x * lookSensitivity * Time.deltaTime;
        float lookY = processedLook.y * lookSensitivity * Time.deltaTime;

        xRotation -= lookY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * lookX);

        Vector3 move = transform.right * moveInput.x + transform.forward * moveInput.y;
        controller.Move(move * speed * Time.deltaTime);

        if (input.JumpPressed)
            Jump();

        if (input.ShootPressed)
            Shoot();

        isGrounded = controller.isGrounded;

        if (isGrounded)
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;

        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
