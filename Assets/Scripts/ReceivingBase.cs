using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceivingBase : MonoBehaviour
{
    private const float GIVE_CONTROL_DELAY = 0.75f;

    [SerializeField]
    private Character _character;

    [SerializeField]
    private CameraFollow _cameraMain;


    private void Start()
    {
        OnStart();
    }


    private void OnStart()
    {
        SetUpCharacter();
        InitReceiveEvents();
    }


    private void InitReceiveEvents()
    {
        var receive = GetComponentsInChildren<Receive>();

        for (int i = 0; i < receive.Length; i++)
        {
            receive[i].OnReceived += OnPassZone;
        }
    }


    private void OnPassZone(Ball ball, ReceivePhase phase)
    {
        switch (phase)
        {
            case ReceivePhase.StartCatch:
                _character.StartAnim(AnimType.Catch);
                _character.StartAnim(AnimType.BallRun, false, true);
                StartCharacterFollowBall(ball);

                break;
            case ReceivePhase.Follow:

                break;
            case ReceivePhase.CatchBall:
                StartCoroutine(CatchBall(ball));

                break;
        }
    }


    private IEnumerator CatchBall(Ball ball)
    {
        _character.ChangeMovableBehaviour(new ForwardMovePattern(_character.transform, _character.MoveSpeed));
        _character.ExecuteMovement();

        _character.StartAnim(AnimType.BallRun, false, true);

        ConfigureCamera();

        StartCoroutine(GiveControl());

        yield return new WaitForSeconds(0.05f);

        _character.StartAnim(AnimType.BallRun, true, true);

        yield return new WaitForSeconds(0.05f);

        ActivateCharacter(ball);
    }


    private void SetUpCharacter()
    {
        _character.ChangeMovableBehaviour(new NonMovePattern());
        _character.ExecuteMovement();

        _character.ChangeTouchMovableBehaviour(new NonTouchMovePattern());
        _character.ExecuteTouchMove();

        _character.StartAnim(AnimType.BallIdle);

        _character.TurnOffContent();
    }


    private void StartCharacterFollowBall(Ball ball)
    {
        _character.ChangeMovableBehaviour(new CharacterFollowBallPattern(ball.transform, _character.transform, _character.MoveSpeed));
        _character.ExecuteMovement();
    }


    private void ConfigureCamera()
    {
        _cameraMain.DeactivateBallTrails();

        _cameraMain.SetFollowableBehaviour(new FollowEntityPattern(_character, _cameraMain.transform));
        _cameraMain.ExecuteFollow();
    }


    private void ActivateCharacter(Ball ball)
    {
        _character.ReceiveBall(ball);
        _character.TurnOnContent();
    }


    private IEnumerator GiveControl()
    {
        yield return new WaitForSeconds(GIVE_CONTROL_DELAY);

        var ch = _character;

        ConfigureCamera();

        ch.ChangeTouchMovableBehaviour(new TouchMovePattern(ch.transform, ch.SmoothMoveTime, ch.LimitMove, ch.Sensivity, ch.FKOffsets));
        ch.ExecuteTouchMove();
    }
}