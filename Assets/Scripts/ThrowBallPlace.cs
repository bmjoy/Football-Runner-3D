using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBallPlace : MonoBehaviour
{
    private const float THROW_DELAY = 1.1f;
    private const float CAMERA_START_FOLLOW_OFFSET = 0.3f;

    [SerializeField]
    private Character _catchTarget;

    [SerializeField]
    private CameraFollow _cameraFollow;

    [SerializeField]
    private Ball _ball;

    private Character _thrower;


    private void OnTriggerEnter(Collider other)
    {
        DetectCharacter(other);
    }


    private void DetectCharacter(Collider other)
    {
        var character = other.GetComponent<Character>();

        if (character != null)
        {
            _thrower = character;

            StartCoroutine(StartThrow());
        }
    }


    private IEnumerator StartThrow()
    {
        SetCameraFollow();
        SetCharacter();

        yield return new WaitForSeconds(THROW_DELAY);

        _cameraFollow.ActivateBallTrails();
    }


    private void SetCameraFollow()
    {
        if (_cameraFollow != null)
        {
            _cameraFollow.SetFollowableBehaviour(new FollowBallPattern(_ball.transform, _cameraFollow.transform, THROW_DELAY - CAMERA_START_FOLLOW_OFFSET, false));
            _cameraFollow.ExecuteFollow();
        }
    }


    private void SetCharacter()
    {
        _thrower.ChangeMovableBehaviour(new CharacterThrowBallTargetPattern(_thrower, _catchTarget, _ball, THROW_DELAY));
        _thrower.ExecuteMovement();

        _thrower.TurnOffContent();
    }
}