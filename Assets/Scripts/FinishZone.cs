using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishZone : MonoBehaviour
{
    private const float KICK_BALL_DELAY = 0.2f;
    private const float MOVE_DELAY = 2.7f;

    [SerializeField]
    private CameraFollow _cameraFollow;

    [SerializeField]
    private BallLauncher _ballLauncher;

    [SerializeField]
    private TapTiming _tapTiming;

    [SerializeField]
    private Ball _ball;


    private void OnTriggerEnter(Collider other)
    {
        DetectCharacter(other);
    }


    private void DetectCharacter(Collider other)
    {
        var character = other.GetComponent<Character>();

        if (character != null)
            StartCoroutine(IEOnDetect(character));
    }


    private IEnumerator IEOnDetect(Character character)
    {
        OnDetectSetUp(character);
        ActivateCharacterKick(character);

        yield return new WaitForSeconds(MOVE_DELAY - KICK_BALL_DELAY);

        DeactivateTapTiming();
        SetCameraFollow();
        SetBallLauncher(character);

        yield return new WaitForSeconds(KICK_BALL_DELAY);

        _cameraFollow.ActivateBallTrails();
    }


    private void OnDetectSetUp(Character character)
    {
        DeactivateCameraAndCharacter(character);
        ActivateTapTiming();
        DeactivatePerformanceOptimization();

        _ball.ClearBehaviours();
    }


    private void SetCameraFollow()
    {
        _cameraFollow.SetFollowableBehaviour(new FollowBallPattern(_ball.transform, _cameraFollow.transform, KICK_BALL_DELAY, true));
        _cameraFollow.ExecuteFollow();
    }


    private void SetBallLauncher(Character character)
    {
        var extraDistance = character.CurrentWeight() + _tapTiming.GetProgress();

        _ballLauncher.Launch(extraDistance);
    }


    private void DeactivateCameraAndCharacter(Character character)
    {
        character.ClearBehaviours();

        _cameraFollow.SetFollowableBehaviour(new NonFollowPattern());
        _cameraFollow.ExecuteFollow();
    }


    private void ActivateCharacterKick(Character character)
    {
        character.ChangeMovableBehaviour(new CharacterThrowBallFinishPattern(character, _ball, MOVE_DELAY));
        character.ExecuteMovement();
    }



    private void DeactivatePerformanceOptimization()
    {
        _cameraFollow.DeactivateOptimization();
    }


    private void ActivateTapTiming()
    {
        _tapTiming.ActivateMechanic();
    }


    private void DeactivateTapTiming()
    {
        _tapTiming.DeactivateMechanic();
    }
}