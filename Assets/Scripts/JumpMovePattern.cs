using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumpMovePattern : MonoBehaviour, IMovable
{
    private int _jumpCount;

    private float _jumpPower;
    private float _jumpDuration;
    private float _jumpLength;

    private Dir _dir;

    private Transform _entityTransform;


    public JumpMovePattern(Transform entityTransform, float jumpDuration, float jumpLength, float jumpPower = 1f, int jumpCount = 1)
    {
        _jumpPower = jumpPower;
        _jumpCount = jumpCount;
        _jumpLength = jumpLength;
        _jumpDuration = jumpDuration;

        _entityTransform = entityTransform;
    }


    public void KillTween()
    {

    }


    private void DefineDirection()
    {
        var forward = _entityTransform.forward;

        if (forward.x > forward.z)
            _dir = Dir.Z;
        else
            _dir = Dir.X;

        //Debug.Log(_dir);
    }



    public IEnumerator Move()
    {
        //DefineDirection();

        var jumpPos = _entityTransform.position;

        switch (_dir)
        {
            case Dir.X:
                jumpPos.z += _jumpLength;

                break;
            case Dir.Z:
                jumpPos.x += _jumpLength;

                break;
        }

        DOJumpMy(_entityTransform, jumpPos, _jumpPower, _jumpCount, _jumpDuration).SetEase(Ease.InSine);

        yield return null;
    }


    private Sequence DOJumpMy(Transform target, Vector3 endValue, float jumpPower, int numJumps, float duration, bool snapping = false)
    {
        if (numJumps < 1)
        {
            numJumps = 1;
        }

        float startPosY = 0f;
        float offsetY = -1f;
        bool offsetYSet = false;
        Sequence s = DOTween.Sequence();
        Tween yTween = DOTween.To(() => target.position, delegate (Vector3 x)
        {
            target.position = x;
        }, new Vector3(0f, jumpPower, 0f), duration / (float)(numJumps * 2)).SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.OutQuad)
            .SetRelative()
            .SetLoops(numJumps * 2, LoopType.Yoyo)
            .OnStart(delegate
            {
                startPosY = target.position.y;
            });
        s.Append(DOTween.To(() => target.position, delegate (Vector3 x)
        {
            target.position = x;
        }, new Vector3(endValue.x, 0f, 0f), duration).SetOptions(AxisConstraint.Y, snapping).SetEase(Ease.Linear)).Join(DOTween.To(() => target.position, delegate (Vector3 x)
        {
            target.position = x;
        }, new Vector3(0f, 0f, endValue.z), duration).SetOptions(AxisConstraint.Z, snapping).SetEase(Ease.Linear)).Join(yTween)
            .SetTarget(target)
            .SetEase(DOTween.defaultEaseType);
        yTween.OnUpdate(delegate
        {
            if (!offsetYSet)
            {
                offsetYSet = true;
                offsetY = (s.isRelative ? endValue.y : (endValue.y - startPosY));
            }

            Vector3 position = target.position;
            position.y += DOVirtual.EasedValue(0f, offsetY, yTween.ElapsedPercentage(), Ease.OutQuad);
            target.position = position;
        });
        return s;
    }
}
