using UnityEngine;
using UnityEngine.UI;

public class DrunkManager : MonoBehaviour
{
    [SerializeField] private Image drunkBarFill;

    [SerializeField] private float drunkValue = 0f;
    [SerializeField] private float drainSpeed = 0.1f;
    [SerializeField] private float addAmount = 0.2f;

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
        UpdateDrunkState();
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

    void UpdateDrunkState()
    {
        if (DrunkStateMachine.Instance == null) return;

        DrunkenState newState;

        if (drunkValue < 0.25f)
            newState = DrunkenState.Sober;
        else if (drunkValue < 0.75f)
            newState = DrunkenState.Tipsy;
        else
            newState = DrunkenState.Drunk;

        if (DrunkStateMachine.Instance.State != newState)
        {
            DrunkStateMachine.Instance.UpdateDrunkenState(newState);
        }
    }
}