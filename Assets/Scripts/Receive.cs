using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ReceivePhase
{
    StartCatch,
    Follow,
    CatchBall
}

public class Receive : MonoBehaviour
{
    [SerializeField]
    private ReceivePhase _receivePhase;

    public event Received OnReceived;
    public delegate void Received(Ball ball, ReceivePhase phase);


    private void OnTriggerEnter(Collider other)
    {
        DetectBall(other);
    }


    private void DetectBall(Collider other)
    {
        var ball = other.GetComponent<Ball>();

        if (ball != null)
        {
            OnDetect(ball);
        }
    }


    private void OnDetect(Ball ball)
    {
        OnReceived?.Invoke(ball, _receivePhase);

        Destroy(gameObject);
    }
}