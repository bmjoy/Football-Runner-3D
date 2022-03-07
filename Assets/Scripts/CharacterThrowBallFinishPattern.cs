using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterThrowBallFinishPattern : MonoBehaviour, IMovable
{
    private const float SPEED_UP_ANIM_TIMING = 2.43f;

    private Character _character;
    private Ball _ball;

    private float _moveTime;


    public CharacterThrowBallFinishPattern(Character character, Ball ball, float moveTime)
    {
        _character = character;
        _ball = ball;

        _moveTime = moveTime;
    }


    public IEnumerator Move()
    {
        CharacterSetUp();
        BallKickPoint();

        yield return new WaitForSeconds(SPEED_UP_ANIM_TIMING);

        ConfigureCharacter();

        yield return new WaitForSeconds(_moveTime - SPEED_UP_ANIM_TIMING);

        _ball.StartFinishFly();
    }


    private void CharacterSetUp()
    {
        _character.StopAnim(AnimType.BallRun, false, false);
        _character.StartAnim(AnimType.KickBall);

        _character.ClearBehaviours();
    }


    private void BallKickPoint()
    {
        var bT = _ball.transform;
        var throwPoint = _character.KickBallPoint.localPosition;

        _ball.transform.parent = _character.KickBallPoint;

        bT.DOLocalMove(throwPoint, _moveTime - (_moveTime - SPEED_UP_ANIM_TIMING));
    }


    private void ConfigureCharacter()
    {
        var speed = _character.GetCurrentAnim().speed;

        var multiplier = 1f / speed;

        _character.SetAnimMultiplier(multiplier);
    }


    public void KillTween()
    {

    }
}
