using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ForwardFollowPlayerPattern : MonoBehaviour, IFollowable
{
    private Transform _obstacle;

    private float _speed;


    public ForwardFollowPlayerPattern(Transform obstacle, float speed = 1.2f)
    {
        _speed = speed;

        _obstacle = obstacle;
    }


    public IEnumerator Follow()
    {
        while (true)
        {
            _obstacle.Translate(-Vector3.forward * _speed * Time.deltaTime);

            yield return null;
        }
    }


    public void KillTween()
    {

    }
}