using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAffector : MonoBehaviour
{
    private const float DETECT_INTERVAL = 0.07f;

    [SerializeField]
    private float _moveSpeed;

    [SerializeField]
    private float _detectRange;

    [SerializeField]
    private float _damage;

    private Character _target;

    private Coroutine _followC;
    private Coroutine _detectC;

    public event Touched OnTouched;
    public delegate void Touched(Character character, bool isMoreWeight);


    public void SetTarget(Character target)
    {
        _target = target;
    }


    public void StartFollow()
    {
        _detectC = StartCoroutine(IEDetectTarget());
    }


    public void StopFollow()
    {
        if (_detectC != null)
            StopCoroutine(_detectC);

        if (_followC != null)
            StopCoroutine(_followC);

    }


    private IEnumerator IEDetectTarget()
    {
        while (true)
        {
            var distance = (_target.transform.position - transform.position).sqrMagnitude;

            if (distance <= _detectRange * _detectRange)
            {
                _followC = StartCoroutine(IEFollowTarget());

                yield break;
            }

            yield return new WaitForSeconds(DETECT_INTERVAL);
        }
    }


    private IEnumerator IEFollowTarget()
    {
        while (true)
        {
            var targetPos = _target.transform.position;

            targetPos.y = transform.position.y;

            var dir = (targetPos - transform.position).normalized;

            transform.Translate(dir * Time.deltaTime * _moveSpeed, Space.World);

            var lookAtPos = new Vector3(targetPos.x, transform.position.y, targetPos.z);

            transform.LookAt(lookAtPos);

            yield return new WaitForEndOfFrame();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        DetectCharacter(other);
    }


    private void DetectCharacter(Collider other)
    {
        var character = other.GetComponent<Character>();

        if (character != null)
            OnDetect(character);
    }


    private void OnDetect(Character character)
    {
        bool isMoreWeight = character.CurrentWeight() > _damage;

        character.ChangeWeight(-_damage);

        OnTouched?.Invoke(character, isMoreWeight);
    }
}
