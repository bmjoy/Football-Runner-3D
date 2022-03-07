using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallHandler : MonoBehaviour
{
    private const float PERCENT_ERROR = 0.085f;

    [SerializeField]
    private float _firingAngle = 45.0f;

    [SerializeField]
    private float _gravity = 9.8f;

    [SerializeField]
    private Ball _ball;

    private Character _receiver;
    private Character _handler;

    private float _flightSpeed = 0.6f; // 0.6f


    private void Start()
    {
        _handler = GetComponent<Character>();
    }


    public void StartThrowSimulation(Character target, Transform entered)
    {
        _receiver = target;

        SetUpProjectile(entered);
        ChangeBallBehaviours();
        StartCoroutine(IESimulateThrow());
    }


    private void SetUpProjectile(Transform entered)
    {
        var ball = _ball.GetComponent<Ball>();
        var character = entered.GetComponent<Progress>();

        ball.SetWeight(character.CurrentWeight);
    }


    private void ChangeBallBehaviours()
    {
        var pr = _ball;

        pr.StartTargetFly();

        pr.ChangeTouchMovableBehaviour(new TouchMovePattern(pr.transform, pr.SmoothMoveTime, pr.LimitMove, pr.Sensivity, null));
        pr.ExecuteTouchMove();
    }


    public void ReceiveBall(Ball ball)
    {
        ball.OnEnd(_handler);

        _ball = ball;
    }


    public void MoveBallToPoint(Vector3 point, float duration)
    {
        if (_ball != null)
            _ball.transform.DOLocalMove(point, duration)/*.SetDelay(0.05f)*/;
    }


    public void RotateBall(Vector3 eulerAngles, float duration)
    {
        if (_ball != null)
            _ball.transform.DOLocalRotate(eulerAngles, duration);
    }


    public void SetBallParent(Transform parent)
    {
        if (_ball != null)
            _ball.transform.parent = parent;
    }


    private IEnumerator IESimulateThrow()
    {
        var bT = _ball.transform;

        bT.parent = null;

        var catchTargetPos = _receiver.ReceiveBallPoint.position;

        catchTargetPos.x = bT.position.x;

        // Move projectile to the position of throwing object + add some offset if needed.
        bT.position = transform.position + new Vector3(0, 1.65f, 0f);

        // Calculate distance to target
        var target_Distance = Vector3.Distance(bT.position, catchTargetPos);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        var projectile_Velocity = target_Distance / (Mathf.Sin(2 * _firingAngle * Mathf.Deg2Rad) / _gravity * _flightSpeed);

        // Extract the X  Y componenent of the velocity
        var Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(_firingAngle * Mathf.Deg2Rad);
        var Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(_firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        var flightDuration = target_Distance / Vx / _flightSpeed;

        // Rotate projectile to face the target.
        bT.rotation = Quaternion.LookRotation(catchTargetPos - bT.position);

        var elapse_time = 0f;

        while (elapse_time < flightDuration)
        {
            if (!_ball.IsAvailable())
            {
                SetParent();

                break;
            }

            CalcRotation(bT, elapse_time, flightDuration);

            var movePos = bT.position + new Vector3(0, (Vy - (_gravity * elapse_time)) * Time.fixedDeltaTime * _flightSpeed,
                Vx * Time.fixedDeltaTime * _flightSpeed);

            movePos.z += _receiver.MoveSpeed / 100 * PERCENT_ERROR;

            bT.position = movePos;

            elapse_time += Time.fixedDeltaTime;

            yield return new WaitForEndOfFrame();
        }

        if (elapse_time >= flightDuration)
            SetParent();
    }


    private void CalcRotation(Transform bT, float elT, float fD)
    {
        var targetRotation = new Vector3(0f, 0f, 0f);

        var xRotation = -_firingAngle + (elT / fD * (_firingAngle * 2));

        targetRotation.x = xRotation;

        bT.eulerAngles = targetRotation;
    }


    private void SetParent()
    {
        _ball.transform.parent = _receiver.HoldBallPoint;
    }
}