using UnityEngine;

public class DetectOptimized : MonoBehaviour
{
    [SerializeField]
    private Collider[] _collider;


    private void Start()
    {
        var detectPhase = GetComponentsInChildren<DetectPhase>();

        for (int i = 0; i < detectPhase.Length; i++)
        {
            detectPhase[i].OnDetectedOptimized += DetectOptimized_OnDetectedOptimized;
        }
    }


    private void DetectOptimized_OnDetectedOptimized(PerformanceOptimization op, ZoneStage zoneStage)
    {
        switch (zoneStage)
        {
            case ZoneStage.Enter:
                op.TurnOnMesh();

                break;
            case ZoneStage.Exit:
                op.TurnOffMesh();

                break;
        }
    }


    public void DeactivateOptimization()
    {
        for (int i = 0; i < _collider.Length; i++)
            _collider[i].enabled = false;
    }
}
