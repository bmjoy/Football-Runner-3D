using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapTiming : MonoBehaviour
{
    private const float ADD_FACTOR = 30f;
    private const float ADD_TIME = 0.1f;
    private const float TAP_COOLDOWN = 0.2f;

    private const float MIN_SCALE = 0.92f;
    private const float ADDITIVE_SCALE = 0.08f;

    [SerializeField]
    private GameObject _content;

    [SerializeField]
    private Image _backProgress;

    [SerializeField]
    private Image _tapFinger;

    [SerializeField, Range(0f, 1f)]
    private float _tapStrength = 0.1f;

    [SerializeField, Range(0f, 1f)]
    private float _reduceFactor = 0.2f;

    [SerializeField]
    private float _scaleSpeed;

    private Coroutine _increaseC;
    private Coroutine _reduceC;
    private Coroutine _detectTapC;
    private Coroutine _tapFingerC;

    private float _currCooldown;
    private float _time;


    private void Start()
    {
        DeactivateMechanic();
    }


    public void ActivateMechanic()
    {
        _content.SetActive(true);

        _detectTapC = StartCoroutine(IEDetectTap());
        _tapFingerC = StartCoroutine(IEScale());
    }


    public void DeactivateMechanic()
    {
        StopAllCoroutines();

        _content.SetActive(false);
    }


    private IEnumerator IEDetectTap()
    {
        while (true)
        {
            _currCooldown -= Time.deltaTime;

            if (Input.GetMouseButtonDown(0) && _currCooldown < 0f)
            {
                OnTap();

                _currCooldown = TAP_COOLDOWN;
            }

            yield return null;
        }
    }


    public void OnTap()
    {
        if (_reduceC != null)
            StopCoroutine(_reduceC);

        if (_increaseC != null)
            StopCoroutine(_increaseC);

        _backProgress.fillAmount += _tapStrength;

        _increaseC = StartCoroutine(IEIncreaseByTap());
    }


    private IEnumerator IEIncreaseByTap()
    {
        float time = 0f;

        while (time < ADD_TIME)
        {
            if (_backProgress.fillAmount < 1)
                _backProgress.fillAmount += _tapStrength * ADD_FACTOR * Time.deltaTime;

            time += Time.deltaTime;

            yield return null;
        }

        _reduceC = StartCoroutine(IEReduceWithTime());
    }


    private IEnumerator IEReduceWithTime()
    {
        while (true)
        {
            if (_backProgress.fillAmount > 0)
                _backProgress.fillAmount -= _reduceFactor * Time.deltaTime;

            yield return null;
        }
    }


    private IEnumerator IEScale(float delay = 0f)
    {
        yield return new WaitForSeconds(delay);

        while (true)
        {
            var scale = Mathf.Cos(_time * _scaleSpeed);

            scale = Mathf.Abs(scale);
            scale = scale * ADDITIVE_SCALE + MIN_SCALE;

            var vScale = new Vector3(scale, scale, scale);

            _tapFinger.rectTransform.localScale = vScale;

            _time += Time.deltaTime;

            yield return null;
        }
    }


    public float GetProgress()
    {
        return _backProgress.fillAmount * 100;
    }
}