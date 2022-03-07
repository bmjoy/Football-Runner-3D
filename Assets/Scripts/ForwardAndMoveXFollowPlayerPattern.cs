using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ForwardAndMoveXFollowPlayerPattern : MonoBehaviour, IFollowable
{
    private const float RIGHT_DIVIDER = 2f;

    private Transform _obstacle;
    private Transform _target;

    private float _speed;


    public ForwardAndMoveXFollowPlayerPattern(Transform target, Transform obstacle, float speed = 1.2f)
    {
        _speed = speed;

        _target = target;
        _obstacle = obstacle;
    }


    public IEnumerator Follow()
    {
        while (true)
        {
            var needPos = -Vector3.forward;

            _obstacle.Translate(needPos * _speed * Time.deltaTime);

            var right = _obstacle.position;

            right.x = _target.position.x / RIGHT_DIVIDER;

            _obstacle.position = Vector3.MoveTowards(_obstacle.position, right, Time.deltaTime);

            yield return null;
        }
    }


    public void KillTween()
    {

    }
}