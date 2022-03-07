using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOverPattern : MonoBehaviour, IPassable
{
    private int _jumpCount;

    private float _jumpPower;
    private float _jumpDuration;
    private float _jumpLength;


    public JumpOverPattern(float jumpDuration, float jumpLength, float jumpPower, int jumpCount)
    {
        _jumpPower = jumpPower;
        _jumpCount = jumpCount;
        _jumpLength = jumpLength;
        _jumpDuration = jumpDuration;
    }


    public IEnumerator Pass(Character ch)
    {
        ch.StopAnim(AnimType.BallRun, false, false);
        ch.StartAnim(AnimType.JumpOver);

        ch.ChangeMovableBehaviour(new JumpMovePattern(ch.transform, _jumpDuration, _jumpLength, _jumpPower, _jumpCount));
        ch.ExecuteMovement();

        yield return new WaitForSeconds(_jumpDuration);

        ch.StartAnim(AnimType.BallRun, true, true);

        ch.SpawnParticles(ParticleType.Landing);

        ch.ChangeMovableBehaviour(new ForwardMovePattern(ch.transform, ch.MoveSpeed));
        ch.ExecuteMovement();
    }
}
