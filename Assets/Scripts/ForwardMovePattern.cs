using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardMovePattern : MonoBehaviour, IMovable
{
    private float _moveSpeed;

    private Transform _entityTransform;


    public ForwardMovePattern(Transform entityTransform, float moveSpeed)
    {
        _moveSpeed = moveSpeed;

        _entityTransform = entityTransform;
    }


    public IEnumerator Move()
    {
        Vector3 localForward = _entityTransform.worldToLocalMatrix.MultiplyVector(_entityTransform.forward);

        while (true)
        {
            _entityTransform.Translate(localForward * Time.deltaTime * _moveSpeed);

            yield return new WaitForEndOfFrame();
        }
    }


    public void KillTween()
    {

    }
}