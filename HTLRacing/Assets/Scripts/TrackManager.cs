using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    public LapManager LapManager;

    public void InitializeTrack()
    {
        LapManager.Initialize();
    }
}
