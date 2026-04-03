using UnityEngine;

public class DrunkStateMachine : MonoBehaviour
{
    public static DrunkStateMachine Instance;

    private DrunkStateBase currentState;

    private SoberState soberState;
    private TipsyState tipsyState;
    private DrunkStateHeavy drunkState;

    private void Awake()
    {
        Instance = this;

        soberState = new SoberState(this);
        tipsyState = new TipsyState(this);
        drunkState = new DrunkStateHeavy(this);
    }

    private void Start()
    {
        ChangeState(soberState);
    }

    private void Update()
    {
        currentState?.Update();
    }

    public void SetStateFromValue(float drunkValue)
    {
        if (drunkValue < 0.25f)
            ChangeState(soberState);
        else if (drunkValue < 0.75f)
            ChangeState(tipsyState);
        else
            ChangeState(drunkState);
    }

    void ChangeState(DrunkStateBase newState)
    {
        if (currentState == newState) return;

        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }
}