using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum TurnDir
{
    Right,
    Left
}

public class TurnPlace : MonoBehaviour
{
    private const float ROTATE_DURATION = 4f;
    private const float ROTATE_ANGLE = 90f;

    [SerializeField]
    private TurnDir _turnDir;

    [SerializeField]
    private CameraFollow _cameraFollow;

    private Coroutine _IERotate;


    private void Start()
    {
        InitZonePhase();
    }


    private void InitZonePhase()
    {
        var zonePhases = GetComponentsInChildren<ZonePhase>();

        for (int i = 0; i < zonePhases.Length; i++)
        {
            zonePhases[i].OnDetectedCharacter += TurnPlace_OnDetected;
        }
    }


    private void TurnPlace_OnDetected(Character character, ZoneStage zoneStage)
    {
        switch (zoneStage)
        {
            case ZoneStage.Enter:
                OnEnter(character);

                break;
            case ZoneStage.Exit:
                OnExit(character);

                break;
        }
    }


    private void OnEnter(Character character)
    {
        character.ChangeTouchMovableBehaviour(new NonTouchMovePattern());
        character.ExecuteTouchMove();

        //_cameraFollow.SetFollowableBehaviour(new NonFollowPattern());
        //_cameraFollow.ExecuteFollow();

        _IERotate = StartCoroutine(IERotate(character));
    }


    private void OnExit(Character ch)
    {
        ch.ChangeTouchMovableBehaviour(new TouchMovePattern(ch.transform, ch.SmoothMoveTime, ch.LimitMove, ch.Sensivity, ch.FKOffsets));
        ch.ExecuteTouchMove();

        _cameraFollow.SetFollowableBehaviour(new FollowEntityPattern(ch, _cameraFollow.transform));
        _cameraFollow.ExecuteFollow();
    }


    private IEnumerator IERotate(Character character)
    {
        var rotate = Vector3.zero;

        switch (_turnDir)
        {
            case TurnDir.Right:
                rotate = new Vector3(0f, ROTATE_ANGLE, 0f);

                _cameraFollow.transform.DOMove(new Vector3(_cameraFollow.transform.position.x, _cameraFollow.transform.position.y, _cameraFollow.transform.position.z + character.MoveSpeed * 2), ROTATE_DURATION);

                break;
            case TurnDir.Left:
                rotate = Vector3.zero;

                break;
        }

        character.transform.DORotate(rotate, ROTATE_DURATION);
        _cameraFollow.transform.DORotate(rotate, ROTATE_DURATION);

        yield return new WaitForSeconds(ROTATE_DURATION);

        OnExit(character);
    }
}