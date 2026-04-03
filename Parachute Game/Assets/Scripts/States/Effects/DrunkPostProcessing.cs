using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DrunkPostProcessing : MonoBehaviour
{
    public static DrunkPostProcessing Instance;

    [SerializeField] private Volume globalVolume;

    private LensDistortion lens;
    private ChromaticAberration chromatic;

    private float targetDistortion;
    private float targetChromatic;

    private float currentDistortion;
    private float currentChromatic;
 

    [SerializeField] private float transitionSpeed = 2f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        globalVolume.profile.TryGet(out lens);
        globalVolume.profile.TryGet(out chromatic);

        if (lens != null) currentDistortion = lens.intensity.value;
        if (chromatic != null) currentChromatic = chromatic.intensity.value;
    
    }

    private void Update()
    {
        float step = transitionSpeed * Time.deltaTime;

        if (lens != null)
        {
            currentDistortion = Mathf.MoveTowards(currentDistortion, targetDistortion, step);
            lens.intensity.value = currentDistortion;
        }

        if (chromatic != null)
        {
            currentChromatic = Mathf.MoveTowards(currentChromatic, targetChromatic, step);
            chromatic.intensity.value = currentChromatic;
        }
    }

    public void SetEffects(float distortion, float chromaticAmount)
    {
        targetDistortion = distortion;
        targetChromatic = chromaticAmount;
    }
}