using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterThrowBallTargetPattern : MonoBehaviour, IMovable
{
    private Ball _ball;

    private Character _character;
    private Character _target;

    private float _throwDelay;


    public CharacterThrowBallTargetPattern(Character character, Character target, Ball ball, float throwDelay)
    {
        _character = character;
        _target = target;

        _ball = ball;

        _throwDelay = throwDelay;
    }


    public IEnumerator Move()
    {
        var bT = _ball.transform;

        _ball.transform.parent = _character.ThrowBallPoint;

        var throwPoint = _character.ThrowBallPoint.localPosition;

        bT.DOLocalMove(throwPoint, 0.2f);

        _character.ClearBehaviours();

        _character.StopAnim(AnimType.BallRun, false, false);
        _character.StartAnim(AnimType.Throw);

        yield return new WaitForSeconds(_throwDelay);

        _character.ThrowBall(_target);
    }


    public void KillTween()
    {

    }
}