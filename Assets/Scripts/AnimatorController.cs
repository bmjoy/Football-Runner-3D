using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum AnimType
{
    None,
    Run,
    BallRun,
    JumpOver,
    Idle,
    BallIdle,
    LookBack,
    Catch,
    Throw,
    CrouchWalk,
    CrouchIdle,
    Fall,
    Walk,
    ModelChange,
    KickBall
}


public class AnimatorController : MonoBehaviour
{
    #region Anim names

    private const string IDLE_1 = "Idle_1";
    private const string RUN_1 = "Run_1";
    private const string CHARACTER_BALL_RUN_UPPER_1 = "CharacterBallRunUpper_1";
    private const string CHARACTER_BALL_RUN_LOWER_1 = "CharacterBallRunLower_1";
    private const string CHARACTER_CATCH_1 = "CharacterCatch_1";
    private const string CHARACTER_BALL_IDLE_1 = "CharacterBallIdle_1";
    private const string CHARACTER_THROW_1 = "CharacterThrow_1";
    private const string KICK_BALL = "KickBall";
    private const string SPEED_MULTIPLIER = "SpeedMultiplier";
    private const string ENEMY_CROUCH_WALK_1 = "EnemyCrouchWalk_1";
    private const string CROUCH_IDLE_1 = "CrouchIdle_1";
    private const string MODEL_CHANGE = "ModelChange_1";
    private const string STOP_MODEL_CHANGE = "StopModelChange_1";
    private const string NONE = "None";

    private readonly string[] CHARACTER_JUMP_OVER = { /*"CharacterFlip_1",*/ /*"CharacterFlip_2", */"CharacterFlip_3" };
    private readonly string[] FALL = { "Fall_1" };
    private readonly string[] WALK = { "Walk_1" };

    #endregion

    private const float RESET_DURATION = 0.1f;
    private const float RESET_DELAY = 0.15f;

    private bool[] _isBallRun = new bool[2];

    private Animator[] _animator;

    private Coroutine _continueRunC;


    private void OnEnable()
    {
        SetAnimators();
    }


    public void SetAnimators()
    {
        _animator = GetComponentsInChildren<Animator>();
    }


    public void StopAnim(AnimType anim, bool value1 = false, bool value2 = false)
    {
        if (_animator == null) return;

        switch (anim)
        {
            case AnimType.BallRun:
                BallRun(value1, value2);

                break;
            case AnimType.ModelChange:
                StopModelChange();

                break;
        }
    }


    public void StartAnim(AnimType anim, bool value1 = false, bool value2 = false)
    {
        if (_animator == null) return;

        ResetTransform(true);

        //if (anim == AnimType.ModelChange)
        //    Debug.Log(anim);

        switch (anim)
        {
            case AnimType.Run:
                Run(value1);

                break;
            case AnimType.Idle:
                Idle();

                break;
            case AnimType.BallIdle:
                BallIdle();

                break;
            case AnimType.JumpOver:
                JumpOver();

                break;
            case AnimType.Catch:
                Catch();

                break;
            case AnimType.Throw:
                Throw();

                break;
            case AnimType.BallRun:
                BallRun(value1, value2);

                break;
            case AnimType.None:
                None();

                break;
            case AnimType.CrouchWalk:
                CrouchWalk(value1);

                break;
            case AnimType.Fall:
                Fall();

                break;
            case AnimType.Walk:
                Walk(value1);

                break;
            case AnimType.CrouchIdle:
                CrouchIdle();

                break;
            case AnimType.ModelChange:
                ModelChange();

                break;
            case AnimType.KickBall:
                KickBall();

                break;
        }
    }


    private void Run(bool isRun)
    {
        for (int i = 0; i < _animator.Length; i++)
            _animator[i].SetBool(RUN_1, isRun);
    }


    private void Throw()
    {
        if (_continueRunC != null)
            StopCoroutine(_continueRunC);

        for (int i = 0; i < _animator.Length; i++)
            _animator[i].SetTrigger(CHARACTER_THROW_1);
    }


    private void None()
    {
        for (int i = 0; i < _animator.Length; i++)
            _animator[i].SetTrigger(NONE);
    }


    private void CrouchWalk(bool isWalk)
    {
        for (int i = 0; i < _animator.Length; i++)
            _animator[i].SetBool(ENEMY_CROUCH_WALK_1, isWalk);
    }


    private void BallRun(bool isUpperRun, bool isLowerRun)
    {
        for (int i = 0; i < _animator.Length; i++)
        {
            _animator[i].SetBool(CHARACTER_BALL_RUN_UPPER_1, isUpperRun);
            _animator[i].SetBool(CHARACTER_BALL_RUN_LOWER_1, isLowerRun);
        }

        _isBallRun[0] = isUpperRun;
        _isBallRun[1] = isLowerRun;
    }


    private void Idle()
    {
        for (int i = 0; i < _animator.Length; i++)
            _animator[i].SetTrigger(IDLE_1);
    }


    private void BallIdle()
    {
        for (int i = 0; i < _animator.Length; i++)
            _animator[i].SetTrigger(CHARACTER_BALL_IDLE_1);
    }


    private void JumpOver()
    {
        StopModelChange();

        if (_continueRunC != null)
            StopCoroutine(_continueRunC);

        var randomIndex = Random.Range(0, CHARACTER_JUMP_OVER.Length);
        var jumpName = CHARACTER_JUMP_OVER[randomIndex];

        for (int i = 0; i < _animator.Length; i++)
            _animator[i].SetTrigger(jumpName);

        ResetTransform(true, 0.15f);
    }


    private void Catch()
    {
        for (int i = 0; i < _animator.Length; i++)
            _animator[i].SetTrigger(CHARACTER_CATCH_1);
    }


    private void Fall()
    {
        var randomIndex = Random.Range(0, FALL.Length);
        var fallName = FALL[randomIndex];

        StopAnim(AnimType.BallRun, false, false);

        for (int i = 0; i < _animator.Length; i++)
            _animator[i].SetTrigger(fallName);
    }


    private void Walk(bool isWalk)
    {
        var randomIndex = Random.Range(0, WALK.Length);
        var walkName = WALK[randomIndex];

        for (int i = 0; i < _animator.Length; i++)
            _animator[i].SetBool(walkName, isWalk);
    }


    private void CrouchIdle()
    {
        for (int i = 0; i < _animator.Length; i++)
            _animator[i].SetTrigger(CROUCH_IDLE_1);
    }


    private void ModelChange()
    {
        foreach (var item in _animator)
        {
            if (item)
            {
                var m_CurrentClipInfo = item.GetCurrentAnimatorClipInfo(0);

                for (int i = 0; i < CHARACTER_JUMP_OVER.Length; i++)
                {
                    if (m_CurrentClipInfo[0].clip.name == CHARACTER_JUMP_OVER[i])
                    {
                        return;
                    }
                }
            }
        }

        _continueRunC = StartCoroutine(IEContinueBallRun());

        StopAnim(AnimType.BallRun, false, false);

        for (int i = 0; i < _animator.Length; i++)
            _animator[i].SetBool(MODEL_CHANGE, true);
    }


    private void KickBall()
    {
        for (int i = 0; i < _animator.Length; i++)
            _animator[i].SetTrigger(KICK_BALL);
    }


    private void StopModelChange()
    {
        for (int i = 0; i < _animator.Length; i++)
            _animator[i].SetBool(MODEL_CHANGE, false);
    }


    private IEnumerator IEContinueBallRun()
    {
        var waitTime = 0.65f;

        yield return new WaitForSeconds(waitTime);

        StopAnim(AnimType.ModelChange);

        ResetTransform(false);

        StartAnim(AnimType.BallRun, true, true);
    }


    private void ResetTransform(bool isWithDelay, float additiveDelay = 0f)
    {
        for (int i = 0; i < _animator.Length; i++)
        {
            var transformE = _animator[i].transform;

            var delay = RESET_DELAY + additiveDelay;

            if (!isWithDelay)
                delay = 0f;

            transformE.DOLocalRotate(Vector3.zero, RESET_DURATION).SetDelay(delay);
            transformE.DOLocalMove(Vector3.zero, RESET_DURATION).SetDelay(delay);
        }
    }


    public void SetAnimMultiplier(float multiplier)
    {
        for (int i = 0; i < _animator.Length; i++)
            _animator[i].SetFloat(SPEED_MULTIPLIER, multiplier);
    }


    public AnimatorStateInfo GetCurrentAnim()
    {
        return _animator[0].GetCurrentAnimatorStateInfo(0);
    }
}