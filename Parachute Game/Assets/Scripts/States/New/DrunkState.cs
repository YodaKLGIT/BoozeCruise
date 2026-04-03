using UnityEngine;

public class DrunkStateHeavy : DrunkStateBase
{
    public DrunkStateHeavy(DrunkStateMachine machine) : base(machine) { }

    public override void Enter()
    {
        Debug.Log("Entered DRUNK");
        DrunkPostProcessing.Instance.SetEffects(-0.9f, 1f); // distortion, chromatic
        CameraWobble.Instance.SetIntensity(1f);
    }
}