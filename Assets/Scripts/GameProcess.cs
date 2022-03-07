using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProcess : MonoBehaviour
{
    [SerializeField]
    private Character _startCharacter;

    [SerializeField]
    private Character[] _character;

    public static GameProcess Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        _startCharacter.StartAnim(AnimType.BallIdle);

        InitCharacters();
    }


    private void InitCharacters()
    {
        for (int i = 0; i < _character.Length; i++)
            _character[i].OnDie += GameProcess_OnDie;
    }


    private void GameProcess_OnDie()
    {
        GameC.Instance.LevelEnd(false);

        for (int i = 0; i < _character.Length; i++)
        {
            var ch = _character[i];

            if (ch != null)
                ch.OnDie -= GameProcess_OnDie;
        }
    }


    public void OnGameStart()
    {
        var ch = _startCharacter;

        ch.ChangeMovableBehaviour(new ForwardMovePattern(ch.transform, ch.MoveSpeed));
        ch.ExecuteMovement();

        ch.ChangeTouchMovableBehaviour(new TouchMovePattern(ch.transform, ch.SmoothMoveTime, ch.LimitMove, ch.Sensivity, ch.FKOffsets));
        ch.ExecuteTouchMove();

        ch.StartAnim(AnimType.BallRun, true, true);
    }
}
