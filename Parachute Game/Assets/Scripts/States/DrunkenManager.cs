using UnityEngine;
using UnityEngine.UI;

public class DrunkManager : MonoBehaviour
{
    [SerializeField] private Image drunkBarFill;

    [SerializeField] private float drunkValue = 0f;
    [SerializeField] private float drainSpeed = 0.1f;
    [SerializeField] private float addAmount = 0.2f;

    public static DrunkManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        ScoreManager.OnPointAdded += HandlePointAdded;
    }

    private void OnDisable()
    {
        ScoreManager.OnPointAdded -= HandlePointAdded;
    }

    void Update()
    {
        DrainDrunkness();

        if (DrunkStateMachine.Instance != null)
        {
            DrunkStateMachine.Instance.SetStateFromValue(drunkValue);
        }
    }

    void HandlePointAdded()
    {
        drunkValue += addAmount;
        drunkValue = Mathf.Clamp01(drunkValue);
    }

    void DrainDrunkness()
    {
        drunkValue -= drainSpeed * Time.deltaTime;
        drunkValue = Mathf.Clamp01(drunkValue);

        if (drunkBarFill != null)
        {
            drunkBarFill.fillAmount = drunkValue;
        }
    }

    public float GetDrunkValue()
    {
        return drunkValue;
    }

    public void ReduceDrunkValue(float amount)
    {
        drunkValue -= amount;
        drunkValue = Mathf.Clamp01(drunkValue);

        if (drunkBarFill != null)
        {
            drunkBarFill.fillAmount = drunkValue;
        }
    }
}