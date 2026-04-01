using System;
using UnityEngine;

public class DrunkState : MonoBehaviour
{
    public static DrunkState Instance;
    public DrunkenState State;

    public static event Action<DrunkenState> OnDrunkenStateChanged;

    private void Awake()
    {
        Instance = this;    
    }

    private void Start()
    {
        UpdateDrunkenState(DrunkenState.Sober);
    }

    public void UpdateDrunkenState(DrunkenState newState)
    {
        State = newState;

        switch (newState)
        {
            case DrunkenState.Sober:
                break;
            case DrunkenState.Tipsy:
                break;
            case DrunkenState.Drunk:
                break;
        }

        OnDrunkenStateChanged?.Invoke(newState);
    }
}

public enum DrunkenState
{
    Sober,
    Tipsy,
    Drunk,
}
