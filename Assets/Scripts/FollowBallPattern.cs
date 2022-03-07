using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowBallPattern : MonoBehaviour, IFollowable
{
    private float _dividerXMovement;
    private float _offsetZ;
    private float _offsetY;
    private float _moveTime;

    private bool _isNullUpdate;

    private Transform _followTarget;
    private Transform _cameraTransform;


    public FollowBallPattern(Transform followTarget, Transform cameraTransform, float moveTime, bool isNullUpdate, float offsetY = 3f, float offsetZ = 8f, float dividerXMovement = 2f)
    {
        _followTarget = followTarget;
        _cameraTransform = cameraTransform;

        _dividerXMovement = dividerXMovement;
        _offsetZ = offsetZ;
        _offsetY = offsetY;
        _moveTime = moveTime;

        _isNullUpdate = isNullUpdate;
    }


    public IEnumerator Follow()
    {
        SmoothMove(_moveTime);

        yield return new WaitForSeconds(_moveTime);

        while (true)
        {
            if (_followTarget == null) break;

            var movePos = GetMovePos();

            _cameraTransform.position = movePos;

            if (_isNullUpdate)
                yield return null;
            else
                yield return new WaitForEndOfFrame();
        }
    }


    private void SmoothMove(float moveTime)
    {
        _cameraTransform.DOMove(GetMovePos(), moveTime);
    }


    private Vector3 GetMovePos()
    {
        var movePos = _followTarget.position;

        movePos.x = _followTarget.position.x / _dividerXMovement;
        movePos.y += _offsetY;
        movePos.z -= _offsetZ;

        return movePos;
    }


    public void KillTween()
    {

    }
}