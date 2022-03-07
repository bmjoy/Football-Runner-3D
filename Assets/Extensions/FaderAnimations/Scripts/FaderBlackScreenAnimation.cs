using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class FaderBlackScreenAnimation : MonoBehaviour, ISceneFaderAnimation
{
    [Header("Settings")]
    [SerializeField] private float fadeTime = 0.5f;

    [Header("Objects")]
    [SerializeField] private Image fadeImage;

    [Header("Other")]
    [SerializeField] private CanvasGroup loadingText;

    #region ShowAndHideMethod
    public void Show(Action callback)
    {
        loadingText.alpha = 1;
        fadeImage.color = fadeImage.color.WithAlpha(1);
        callback?.Invoke();
    }
    public void Hide(Action callback)
    {
        loadingText.DOFade(0, fadeTime);
        fadeImage.DOFade(0f, fadeTime).OnComplete(() => callback?.Invoke());
    }
    public void ShowImmediately()
    {
        gameObject.SetActive();
        fadeImage.color = fadeImage.color.WithAlpha(1);
        loadingText.alpha = 1;
    }
    public void HideImmediately()
    {
        fadeImage.color = fadeImage.color.WithAlpha(0);
        loadingText.alpha = 0;
    }
    #endregion
}