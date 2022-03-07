using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPhase : MonoBehaviour
{
    [SerializeField]
    private ZoneStage _zoneStage;

    public event DetectedOptimized OnDetectedOptimized;
    public delegate void DetectedOptimized(PerformanceOptimization optimization, ZoneStage zoneStage);


    private void OnTriggerEnter(Collider other)
    {
        DetectOptimized(other);
    }


    private void DetectOptimized(Collider other)
    {
        var optimization = other.GetComponent<PerformanceOptimization>();

        if (optimization != null)
            OnDetectOptimized(optimization);
    }


    private void OnDetectOptimized(PerformanceOptimization optimization)
    {
        OnDetectedOptimized?.Invoke(optimization, _zoneStage);
    }
}