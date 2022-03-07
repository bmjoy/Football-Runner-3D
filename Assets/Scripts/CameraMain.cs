using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CameraMain : MonoBehaviour
{
    [SerializeField]
    protected Entity _firstTarget;

    private IFollowable _followable;

    private Coroutine _previousFollow;


    public void SetFollowableBehaviour(IFollowable followable)
    {
        if (_previousFollow != null)
            StopCoroutine(_previousFollow);

        _followable = followable;
    }


    public void SetTarget(Entity target)
    {
        _firstTarget = target;
    }


    public void ExecuteFollow()
    {
        _previousFollow = StartCoroutine(_followable.Follow());
    }


    protected abstract void InitBehaviours();
}