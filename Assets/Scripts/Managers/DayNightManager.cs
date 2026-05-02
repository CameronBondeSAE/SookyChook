    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class DayNightManager : ManagerBase<DayNightManager>
{
    // Set the value of each phase to the time it should start
    // NOTE: Does not work with non-whole-hour times (e.g. 7:30)
    public enum DayPhase
    {
        Midnight = 0,
        Dawn = 5,
        Morning = 7,
        Noon = 12,
        Evening = 17,
        Night = 21
    }
    
    [Tooltip("Set this to the starting state, it will overwrite the time and call the phase's event")]
    private NetworkVariable<int> _currentPhaseNetwork = new NetworkVariable<int>((int)DayPhase.Morning, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    
    public DayPhase currentPhase
    {
        get => (DayPhase)_currentPhaseNetwork.Value;
        private set => _currentPhaseNetwork.Value = (int)value;
    }

    [Tooltip("Measured as hours in 24h format, i.e. value of 17f = 5pm")]
    public NetworkVariable<float> currentTime = new NetworkVariable<float>(7f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    [Tooltip("How many real-time seconds it takes for 1 in-game hour")]
    public float timeDilation = 10f;
    
    /// <summary>
    /// Use if statement to check what phase has just started
    /// E.g. "if phase == DayNightManager.DayPhase.Morning" for morning functions
    /// </summary>
    public event Action<DayPhase> PhaseChangeEvent;

    public override void Awake()
    {
        base.Awake();
        
        // Subscribe to network variable changes to trigger events on clients
        _currentPhaseNetwork.OnValueChanged += OnPhaseChanged;
    }

    private void OnPhaseChanged(int previousValue, int newValue)
    {
        // Trigger the event on all clients when phase changes
        PhaseChangeEvent?.Invoke((DayPhase)newValue);
    }

    private void Start()
    {
        if (IsServer)
        {
            ChangePhase(DayPhase.Morning);
        }
    }

    private void Update()
    {
        // Only the server updates the time
        if (!IsServer) return;
        
        // Check to ensure no divide by zero errors
        if (timeDilation > 0)
        {
            currentTime.Value += Time.deltaTime / timeDilation;
        }

        // Wraps time back to 0 when it hits midnight
        if (currentTime.Value >= 24f)
        {
            ChangePhase(DayPhase.Midnight);
        }
        
        foreach (DayPhase phase in Enum.GetValues(typeof(DayPhase)))
        {
            // Checks if time has passed the phase time and that phase is next in the sequence
            // More modular than a bunch of if statements, just add a phase to the enum and this will include it
            if (currentTime.Value >= (float) phase && (int) phase > (int) currentPhase)
            {
                ChangePhase(phase);
            }
        }
    }

    public void ChangePhase(DayPhase newPhase)
    {
        if (!IsServer) return; // Only server can change phase
        
        currentPhase = newPhase;
        currentTime.Value = (float) newPhase;
        // PhaseChangeEvent will be triggered automatically via OnValueChanged
        // print(newPhase);
    }
}