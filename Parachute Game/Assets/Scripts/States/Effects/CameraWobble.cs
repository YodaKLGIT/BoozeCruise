using UnityEngine;

public class CameraWobble : MonoBehaviour
{
    public static CameraWobble Instance;

    [SerializeField] private float wobbleSpeed = 2f;
    [SerializeField] private float rotationAmount = 2f;
    [SerializeField] private float positionAmount = 0.05f;

    private float targetIntensity = 0f;
    private float currentIntensity = 0f;

    private Vector3 startPos;
    private Quaternion startRot;

    private float time;

    private void Awake()
    {
        Instance = this;

        startPos = transform.localPosition;
        startRot = transform.localRotation;
    }

    void Update()
    {
        time += Time.deltaTime * wobbleSpeed;

        currentIntensity = Mathf.Lerp(currentIntensity, targetIntensity, Time.deltaTime * 3f);

        float sin = Mathf.Sin(time);
        float cos = Mathf.Cos(time * 0.7f);

        // Rotation wobble
        float rotZ = sin * rotationAmount * currentIntensity;
        float rotX = cos * rotationAmount * 0.5f * currentIntensity;

        transform.localRotation = startRot * Quaternion.Euler(rotX, 0f, rotZ);

        // Position wobble
        float posX = sin * positionAmount * currentIntensity;
        float posY = cos * positionAmount * currentIntensity;

        transform.localPosition = startPos + new Vector3(posX, posY, 0f);
    }

    public void SetIntensity(float value)
    {
        targetIntensity = value;
    }
}