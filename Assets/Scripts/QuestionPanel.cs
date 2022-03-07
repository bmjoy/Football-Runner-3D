using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class QuestionPanel : MonoBehaviour
{
    private const float MOVE_TIME = 0.8f;
    private const float MIN_SCALE = 0.92f;
    private const float ADDITIVE_SCALE = 0.08f;
    private const float SCALE_SPEED = 1.5f;

    [SerializeField]
    private Image _questionImage;

    [SerializeField]
    private TextMeshProUGUI _questionText;

    [SerializeField]
    private RectTransform _startPos;

    [SerializeField]
    private RectTransform _movePos;

    private Coroutine _scaleC;

    private float _time;

    public static QuestionPanel Instance { get; private set; }


    private void Awake()
    {
        Instance = this;
    }


    private void Start()
    {
        GameC.Instance.OnLevelEnd += Instance_OnLevelEnd;

        MoveToStartPos();
    }


    private void Instance_OnLevelEnd(bool isWin)
    {
        if (!isWin)
        {
            DeactivatePanel();
        }

        GameC.Instance.OnLevelEnd -= Instance_OnLevelEnd;
    }


    private void MoveToStartPos()
    {
        _questionImage.rectTransform.position = _startPos.position;
    }


    public void ActivatePanel(string text)
    {
        _questionText.text = text;

        _questionText.gameObject.SetActive(true);
        _questionImage.gameObject.SetActive(true);

        AppearEffect();
    }


    private void AppearEffect()
    {
        _questionImage.rectTransform.DOMove(_movePos.position, MOVE_TIME);

        _scaleC = StartCoroutine(IEScale(MOVE_TIME));
    }


    private IEnumerator IEScale(float delay)
    {
        yield return new WaitForSeconds(delay);

        while (true)
        {
            ScaleQuestionPanel();

            yield return null;
        }
    }


    private void ScaleQuestionPanel()
    {
        var scale = Mathf.Cos(_time * SCALE_SPEED);

        scale = Mathf.Abs(scale);
        scale = scale * ADDITIVE_SCALE + MIN_SCALE;

        var vScale = new Vector3(scale, scale, scale);

        _questionImage.rectTransform.localScale = vScale;

        _time += Time.deltaTime;
    }


    public void DeactivatePanel()
    {
        if (_scaleC != null)
            StopCoroutine(_scaleC);

        if (_questionImage == null || _questionText == null) return;

        _questionImage.rectTransform.position = _startPos.position;

        _questionImage.gameObject.SetActive(false);
        _questionText.gameObject.SetActive(false);
    }
}