using UnityEngine;

public abstract class DrunkStateBase
{
    protected DrunkStateMachine machine;

    public DrunkStateBase(DrunkStateMachine machine)
    {
        this.machine = machine;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
}
