using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PerfectText : MonoBehaviour
{
    private const float MOVE_TIME = 0.8f;
    private const float LIFE_TIME = 1.7f;

    [SerializeField]
    private PerfectTextParam _textParam;

    [SerializeField]
    private Transform _startPos;

    [SerializeField]
    private Transform[] _movePos;

    private GameObject _previousText;


    public void ShowPerfectText(TextType type)
    {
        Transform textT = null;

        switch (type)
        {
            case TextType.Good:
                textT = GetRandomText(_textParam.GoodText).transform;

                break;
            case TextType.Bad:
                textT = GetRandomText(_textParam.BadText).transform;

                break;
        }

        StartCoroutine(ProcessText(textT));
    }


    private GameObject GetRandomText(GameObject[] text)
    {
        var randIndex = Random.Range(0, text.Length);

        var randText = text[randIndex];

        return randText;
    }


    private IEnumerator ProcessText(Transform textT)
    {
        if (_previousText != null)
            _previousText.SetActive(false);

        _previousText = textT.gameObject;

        textT.transform.position = _startPos.position;
        textT.gameObject.SetActive(true);

        var randIndex = Random.Range(0, _movePos.Length);
        var randPos = _movePos[randIndex].localPosition;

        textT.DOLocalMove(randPos, MOVE_TIME);

        yield return new WaitForSeconds(LIFE_TIME);

        textT.gameObject.SetActive(false);
    }
}