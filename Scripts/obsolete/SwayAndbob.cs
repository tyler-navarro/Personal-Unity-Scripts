using UnityEngine;

public class WeaponBobSway1 : MonoBehaviour
{
    public float bobFrequency = 1f;
    public float bobAmount = 0.02f;
    public float swayAmount = 1f;
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

        float swayX = Mathf.Clamp(-mouseX * swayAmount, -swayAmount, swayAmount);
        float swayY = Mathf.Clamp(-mouseY * swayAmount, -swayAmount, swayAmount);

        Vector3 targetSway = new Vector3(swayX, swayY, 0f);
        currentSway = Vector3.Lerp(currentSway, targetSway, Time.deltaTime * smoothing);

        transform.localRotation = Quaternion.Euler(currentSway);
    }
}