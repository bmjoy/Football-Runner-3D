using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterFollowBallPattern : MonoBehaviour, IMovable
{
    private Transform _ball;
    private Transform _character;

    private float _startFollowDelay;
    private float _moveSpeed;

    private Tween _moveXTween;


    public CharacterFollowBallPattern(Transform ball, Transform character, float moveSpeed = 3f, float startFollowDelay = 0.1f)
    {
        _ball = ball;
        _character = character;

        _startFollowDelay = startFollowDelay;
        _moveSpeed = moveSpeed;
    }


    public IEnumerator Move()
    {
        while (true)
        {
            if (_ball == null) break;

            KillTween();

            var movePos = new Vector3(_character.position.x, _character.position.y, _character.position.z);

            movePos.x = _ball.position.x;

            _moveXTween = _character.DOMove(movePos, _startFollowDelay);

            _character.Translate(_character.forward * Time.fixedDeltaTime * _moveSpeed);

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