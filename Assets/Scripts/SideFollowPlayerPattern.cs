using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SideFollowPlayerPattern : MonoBehaviour, IFollowable
{
    private Transform _target;
    private Transform _obstacle;

    private float _timePerMeter;

    private Tween _moveXTween;


    public SideFollowPlayerPattern(Transform target, Transform obstacle, float timePerMeter = 1.2f)
    {
        _timePerMeter = timePerMeter;

        _target = target;
        _obstacle = obstacle;
    }


    public IEnumerator Follow()
    {
        while (true)
        {
            if (_target == null) break;

            KillTween();

            var movePos = new Vector3(_obstacle.position.x, _obstacle.position.y, _obstacle.position.z);

            movePos.x = _target.position.x;

            var distance = Mathf.Abs(_obstacle.position.x - _target.position.x);

            var moveTime = distance * _timePerMeter;

            _moveXTween = _obstacle.DOMove(movePos, moveTime);

            yield return null;
        }
    }


    public void KillTween()
    {
        if (_moveXTween != null)
        {
            _moveXTween.Kill();

            _moveXTween = null;
        }
    }
}