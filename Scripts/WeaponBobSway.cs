using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBobSway : MonoBehaviour
{
    public float walkBobFrequency = 1f;
    public float walkBobAmount = 0.02f;
    public float walkSwayAmount = 1f;
    public float walkSwaySpeed = 2f;

    public float sprintBobFrequency = 2f;
    public float sprintBobAmount = 0.04f;
    public float sprintSwayAmount = 2f;
    public float sprintSwaySpeed = 4f;

    public float mouseLookSwayAmount = 1f;  // New variable for left-to-right and right-to-left sway
    public float leftToRightSwayFineTune = 1f; // New parameter for fine-tuning left-to-right sway
    public float smoothing = 6f;

    private Vector3 initialPosition;
    private Vector3 targetBob;
    private Vector3 currentSway;
    private float timer = 0f;

    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        HandleBobbing();
        HandleSway();
    }

    void HandleBobbing()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        float bobFrequency = isSprinting ? sprintBobFrequency : walkBobFrequency;
        float bobAmount = isSprinting ? sprintBobAmount : walkBobAmount;

        if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
        {
            timer += Time.deltaTime * bobFrequency;
            float bobX = Mathf.Sin(timer) * bobAmount;
            float bobY = Mathf.Cos(timer * 2f) * bobAmount;

            targetBob = new Vector3(bobX, bobY, 0f);
        }
        else
        {
            timer = 0f;
            targetBob = Vector3.zero;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition + targetBob, Time.deltaTime * smoothing);
    }

    void HandleSway()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        float swayAmount = isSprinting ? sprintSwayAmount : walkSwayAmount;
        float swaySpeed = isSprinting ? sprintSwaySpeed : walkSwaySpeed;

        float swayX = Mathf.Clamp(-mouseX * swayAmount, -swayAmount, swayAmount);
        float swayY = Mathf.Clamp(-mouseY * swayAmount, -swayAmount, swayAmount);

        float swayZ = Mathf.Sin(Time.time * swaySpeed) * swayAmount;

        // Additional left-to-right sway based on mouse movement
        float additionalSwayX = Mathf.Clamp(-mouseX * mouseLookSwayAmount * leftToRightSwayFineTune, -mouseLookSwayAmount, mouseLookSwayAmount);

        // Additional up-and-down sway based on mouse movement (keep this line as it is)
        float additionalSwayY = Mathf.Clamp(-mouseY * mouseLookSwayAmount, -mouseLookSwayAmount, mouseLookSwayAmount);

        Vector3 targetSway = new Vector3(swayX + additionalSwayX, swayY + additionalSwayY, swayZ);
        currentSway = Vector3.Lerp(currentSway, targetSway, Time.deltaTime * smoothing);

        transform.localRotation = Quaternion.Euler(currentSway);
    }
}