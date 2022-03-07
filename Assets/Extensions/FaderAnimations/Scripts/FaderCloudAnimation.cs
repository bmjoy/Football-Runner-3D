using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaderCloudAnimation : MonoBehaviour, ISceneFaderAnimation
{
    [Serializable]
    public struct CloudStartPos
    {
        public Image cloud;
        public float startPosX;
    }
    [Header("Settings")]
    [SerializeField] private Vector2 minMaxMoveTime;

    [Header("Objects")]
    [SerializeField] private List<Image> leftParts = new List<Image>();
    [SerializeField] private List<Image> rightParts = new List<Image>();
    [SerializeField] private Image bgImage;

    [Header("Other")]
    [SerializeField] private CanvasGroup loadingText;

    private List<CloudStartPos> startLeftPartPos = new List<CloudStartPos>();
    private List<CloudStartPos> startRightPartPos = new List<CloudStartPos>();

    private void Awake()
    {
        foreach (var item in leftParts)
        {
            CloudStartPos temp;
            temp.cloud = item;
            temp.startPosX = item.GetRectTransform().anchoredPosition.x;

            startLeftPartPos.Add(temp);
        }
        foreach (var item in rightParts)
        {
            CloudStartPos temp;
            temp.cloud = item;
            temp.startPosX = item.GetRectTransform().anchoredPosition.x;

            startRightPartPos.Add(temp);
        }
    }

    #region ShowAndHideMethod
    public void Hide(Action callback)
    {
        StopAllCoroutines();
        StartCoroutine(Animation(true, Help));
        void Help()
        {
            callback?.Invoke();
        }
    }
    public void HideImmediately()
    {
        StopAllCoroutines();
        bgImage.color = bgImage.color.WithAlpha(0);
        loadingText.alpha = 0;
        foreach (var item in startLeftPartPos)
        {
            RectTransform itemRect = item.cloud.GetRectTransform();
            itemRect.anchoredPosition = new Vector2(0, itemRect.anchoredPosition.y);
        }
        foreach (var item in startRightPartPos)
        {
            RectTransform itemRect = item.cloud.GetRectTransform();
            itemRect.anchoredPosition = new Vector2(0, itemRect.anchoredPosition.y);
        }
    }
    public void Show(Action callback)
    {
        StopAllCoroutines();
        StartCoroutine(Animation(false, Help));
        void Help()
        {
            callback?.Invoke();
        }
    }
    public void ShowImmediately()
    {
        gameObject.SetActive();
        StopAllCoroutines();
        bgImage.color = bgImage.color.WithAlpha(1);
        loadingText.alpha = 1;
        foreach (var item in startLeftPartPos)
        {
            RectTransform itemRect = item.cloud.GetRectTransform();
            itemRect.anchoredPosition = new Vector2(item.startPosX, itemRect.anchoredPosition.y);
        }
        foreach (var item in startRightPartPos)
        {
            RectTransform itemRect = item.cloud.GetRectTransform();
            itemRect.anchoredPosition = new Vector2(item.startPosX, itemRect.anchoredPosition.y);
        }
    }
    #endregion
    #region Animation
    IEnumerator Animation(bool isHide, Action callback)
    {
        bgImage.DOFade((isHide) ? 0 : 1, minMaxMoveTime.y);
        loadingText.DOFade((isHide) ? 0 : 1, minMaxMoveTime.y);
        foreach (var item in startLeftPartPos)
        {
            //item.cloud.GetRectTransform().DOKill();
            float targetPosX = isHide ? 0 : item.startPosX;
            float time = minMaxMoveTime.GetRandom();

            item.cloud.GetRectTransform().DOAnchorPosX(targetPosX, time);
        }
        foreach (var item in startRightPartPos)
        {
            //item.cloud.GetRectTransform().DOKill();
            float targetPosX = isHide ? 0 : item.startPosX;
            float time = minMaxMoveTime.GetRandom();

            item.cloud.GetRectTransform().DOAnchorPosX(targetPosX, time);
        }
        yield return new WaitForSeconds(minMaxMoveTime.y);
        callback?.Invoke();
    }
    #endregion
}
