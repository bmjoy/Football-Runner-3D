using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : CameraMain
{
    [SerializeField]
    private DetectOptimized _detectOptimized;
    
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Transform _colliderPos;

    [SerializeField]
    private GameObject _ballTrail;

    private static Camera s_camera;


    private void OnEnable()
    {
        InitBehaviours();
    }


    private void Start()
    {
        OnStart();
    }


    private void OnStart()
    {
        s_camera = _camera;

        ExecuteFollow();

        StartCoroutine(IEFollowTarget());
    }


    protected override void InitBehaviours()
    {
        SetFollowableBehaviour(new FollowEntityPattern(_firstTarget, transform));
    }


    public void DeactivateOptimization()
    {
        _detectOptimized.DeactivateOptimization();
    }


    public void ActivateBallTrails()
    {
        _ballTrail.SetActive(true);
    }


    public void DeactivateBallTrails()
    {
        _ballTrail.SetActive(false);
    }


    private IEnumerator IEFollowTarget()
    {
        while (true)
        {
            if (_firstTarget != null)
                _colliderPos.position = _firstTarget.transform.position;

            yield return new WaitForEndOfFrame();
        }
    }


    public static Camera Camera { get => s_camera; }
}