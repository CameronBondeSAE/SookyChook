using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    [Tooltip("Measured as hours in 24h format, i.e. value of 17f = 5pm")]
    public float currentTime = 7f;
    
    [Tooltip("How many real-time seconds it takes for 1 in-game hour")]
    public float timeRate = 10f;

    // Set the value of each phase to the time it should start
    // NOTE: Does not work with non-whole-hour times (e.g. 7:30)
    public enum DayPhase
    {
        Morning = 7,
        Noon = 12,
        Evening = 17,
        Night = 21
    }

    public DayPhase currentPhase;
    
    /// <summary>
    /// Use if statement to check what phase has just started
    /// E.g. "if phase == DayNightManager.DayPhase.Morning" for morning functions
    /// </summary>
    public event Action<DayPhase> PhaseChangeEvent;
    
    private void Update()
    {
        // Check to ensure no divide by zero errors
        if (timeRate > 0)
        {
            currentTime += Time.deltaTime / timeRate;
        }
        
        // Wraps time back to 0 when it hits midnight
        if (currentTime >= 24f)
        {
            currentTime = 0;
        }

        foreach (DayPhase phase in Enum.GetValues(typeof(DayPhase)))
        {
            if (currentTime >= (float) phase && phase != currentPhase)
            {
                currentPhase = phase;
                PhaseChangeEvent?.Invoke(phase);
            }
        }
    }
}
