using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DrunkPostProcessing : MonoBehaviour
{
    [SerializeField] private Volume globalVolume;

    private LensDistortion lensDistortion;

    private float targetIntensity;
    private float currentIntensity;

    [SerializeField] private float transitionSpeed = 2f;

    private void Start()
    {
        if (globalVolume.profile.TryGet(out lensDistortion))
        {
            currentIntensity = lensDistortion.intensity.value;
        }
    }

    private void OnEnable()
    {
        DrunkStateMachine.OnDrunkenStateChanged += OnDrunkStateChanged;
    }

    private void OnDisable()
    {
        DrunkStateMachine.OnDrunkenStateChanged -= OnDrunkStateChanged;
    }

    private void Update()
    {
        currentIntensity = Mathf.MoveTowards(
            currentIntensity,
            targetIntensity,
            transitionSpeed * Time.deltaTime
        );

        lensDistortion.intensity.value = currentIntensity;
    }

    void OnDrunkStateChanged(DrunkenState newState)
    {
        switch (newState)
        {
            case DrunkenState.Sober:
                targetIntensity = 0f;
                break;
            case DrunkenState.Tipsy:
                targetIntensity = -0.65f;
                break;
            case DrunkenState.Drunk:
                targetIntensity = -0.95f;
                break;
        }
    }
}
