using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TapToPlay : MonoBehaviour
{
    [SerializeField]
    private GameObject _shopContent;
    
    [SerializeField]
    private Image _playImage;

    [SerializeField]
    private Button _button;

    [SerializeField]
    private float _changeAlphaSpeed;

    [SerializeField]
    private GameObject _currentLevel;

    private Coroutine _changeAlphaCoroutine;


    private void OnEnable()
    {
        GameC.Instance.OnLevelLoaded += OnRestart;
    }


    private void OnRestart(int level)
    {
        ChangeTapState(true);
    }


    public void OnPlayClick()
    {
        ChangeTapState(false);

        GameProcess.Instance.OnGameStart();
    }


    private void ChangeTapState(bool isTurnOn)
    {
        _button.gameObject.SetActive(isTurnOn);
        _playImage.gameObject.SetActive(isTurnOn);
        _currentLevel.SetActive(isTurnOn);
        _shopContent.SetActive(isTurnOn);

        if (isTurnOn)
            _changeAlphaCoroutine = StartCoroutine(ChangeAlpha());
        else
            StopCoroutine(_changeAlphaCoroutine);
    }


    private IEnumerator ChangeAlpha()
    {
        while (true)
        {
            float d = Mathf.Sin(Time.time * _changeAlphaSpeed);

            var color = _playImage.color;
            color.a = Mathf.Abs(d);

            _playImage.color = color;

            yield return new WaitForEndOfFrame();
        }
    }
}