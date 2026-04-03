using UnityEngine;

public class SoberState : DrunkStateBase
{
    public SoberState(DrunkStateMachine machine) : base(machine) { }

    public override void Enter()
    {
        Debug.Log("Entered Sober");
        DrunkPostProcessing.Instance.SetEffects(0f, 0f); // distortion, chromatic
        CameraWobble.Instance.SetIntensity(0f);
    }
}

