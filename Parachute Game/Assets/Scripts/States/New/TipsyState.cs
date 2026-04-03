using UnityEngine;

public class TipsyState : DrunkStateBase
{
    public TipsyState(DrunkStateMachine machine) : base(machine) { }

    public override void Enter()
    {
        Debug.Log("Entered Tipsy");
        DrunkPostProcessing.Instance.SetEffects(-0.65f, 0.5f); // distortion, chromatic
        CameraWobble.Instance.SetIntensity(0.25f);
    }
}
