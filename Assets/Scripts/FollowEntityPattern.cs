using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FollowEntityPattern : MonoBehaviour, IFollowable
{
    private const float MAX_DISTANCE = 8f;
    private const float SMOOTH_MOVE_TIME = 0.25f;

    private Entity _target;
    private Transform _follower;

    private Tween _followT;

    private float _dividerXMovement;
    private float _offsetY;
    private float _offsetZ;


    public FollowEntityPattern(Entity followTarget, Transform cameraTransform, float offsetY = 3.33f, float offsetZ = 5.5f, float dividerXMovement = 2f)
    {
        _target = followTarget;
        _follower = cameraTransform;

        _dividerXMovement = dividerXMovement;
        _offsetY = offsetY;
        _offsetZ = offsetZ;
    }


    public IEnumerator Follow()
    {
        var a = _target.transform.position;
        var b = _follower.position;

        var distance = Vector3.Distance(a, b);

        if (distance >= MAX_DISTANCE)
        {
            MoveSmoothly(SMOOTH_MOVE_TIME);

            yield return new WaitForSeconds(SMOOTH_MOVE_TIME);
        }

        yield return new WaitForEndOfFrame();


        while (true)
        {
            KillTween();

            var movePos = GetMovePos();

            _follower.position = movePos;

            yield return null;
        }
    }


    private void MoveSmoothly(float moveTime)
    {
        var movePos = GetMovePos();

        movePos.z += moveTime * _target.MoveSpeed;

        _follower.DOMove(movePos, moveTime);
    }


    private Vector3 GetMovePos()
    {
        var movePos = _target.transform.position;

        movePos.x = _target.transform.position.x / _dividerXMovement;
        movePos.y = _offsetY;
        movePos.z -= _offsetZ;

        return movePos;
    }


    public void KillTween()
    {
        if (_followT != null)
        {
            _followT.Kill();
        }
    }
}