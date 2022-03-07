using UnityEngine;
using System.Collections;

public class BallLauncher : MonoBehaviour
{
    private const float DRAG_DIVIDER = 12f;
    private const float END_SPEED_TO_WIN = 1f;
    private const float END_DRAG = 8f;
    private const float DRAG_START_DELAY = 5f;

    [SerializeField]
    private Rigidbody _ball;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _height = 25f;

    [SerializeField]
    private float _gravity = -18f;

    [SerializeField]
    private bool _debugPath;


    private void Start()
    {
        _ball.useGravity = false;
    }


    private void Update()
    {
        if (_debugPath)
        {
            DrawPath();
        }
    }

    /// <summary>
    /// Add physic velocity to ball
    /// </summary>
    /// <param name="extraDistance"> extra distance from some upgrades </param>
    public void Launch(float extraDistance = 0f)
    {
        ChangeTargetPos(extraDistance);

        var ball = _ball.GetComponent<Ball>();

        _ball.isKinematic = false;
        _ball.collisionDetectionMode = CollisionDetectionMode.Discrete;

        ball.transform.parent = null;

        Physics.gravity = Vector3.up * _gravity;
        _ball.useGravity = true;
        _ball.velocity = CalculateLaunchData().initialVelocity;

        StartCoroutine(IEIncreaseDragWithTime());
    }


    private IEnumerator IEIncreaseDragWithTime()
    {
        yield return new WaitForSeconds(DRAG_START_DELAY);

        while (true)
        {
            _ball.drag += Time.deltaTime / DRAG_DIVIDER;

            if (CheckBallVelocity())
                yield break;

            yield return null;
        }
    }


    private bool CheckBallVelocity()
    {
        if (_ball.velocity.magnitude <= END_SPEED_TO_WIN)
        {
            _ball.drag += END_DRAG;

            GameC.Instance.LevelEnd(true);

            return true;
        }

        return false;
    }


    private void ChangeTargetPos(float extraDistance)
    {
        var targetPos = _target.position;

        targetPos.z += extraDistance;

        _target.position = targetPos;
    }

    LaunchData CalculateLaunchData()
    {
        float displacementY = _target.position.y - _ball.position.y;
        Vector3 displacementXZ = new Vector3(_target.position.x - _ball.position.x, 0, _target.position.z - _ball.position.z);
        float time = Mathf.Sqrt(-2 * _height / _gravity) + Mathf.Sqrt(2 * (displacementY - _height) / _gravity);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * _gravity * _height);
        Vector3 velocityXZ = displacementXZ / time;

        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(_gravity), time);
    }

    void DrawPath()
    {
        LaunchData launchData = CalculateLaunchData();
        Vector3 previousDrawPoint = _ball.position;

        int resolution = 30;
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget;
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * _gravity * simulationTime * simulationTime / 2f;
            Vector3 drawPoint = _ball.position + displacement;
            Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
            previousDrawPoint = drawPoint;
        }
    }

    struct LaunchData
    {
        public readonly Vector3 initialVelocity;
        public readonly float timeToTarget;

        public LaunchData(Vector3 initialVelocity, float timeToTarget)
        {
            this.initialVelocity = initialVelocity;
            this.timeToTarget = timeToTarget;
        }
    }
}
