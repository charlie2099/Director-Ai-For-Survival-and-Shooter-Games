using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectorState : MonoBehaviour
{
    public enum Phase 
    {
        RESPITE,
        BUILD_UP,
        PEAK
    }

    private Phase currentPhase;

    private void Awake()
    {
        currentPhase = Phase.RESPITE;
    }
}

