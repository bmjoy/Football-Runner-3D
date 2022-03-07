using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaderWaveAnimation : MonoBehaviour, ISceneFaderAnimation
{
    [Header("Prefabs")]
    [SerializeField] private List<Image> wavePrefabs;

    [Header("Objects")]
    [SerializeField] private VerticalLayoutGroup waveParrent;

    [Header("Settings")]
    [SerializeField] private float animationTime;
    [SerializeField] private float waveCount;

    [Header("Other")]
    [SerializeField] private CanvasGroup loadingText;

    private List<Image> parts = new List<Image>();

    private void Awake()
    {
        Generate();
    }

    #region StartGeneration
    private void Generate()
    {
        int waveSpawnIndex = 0;
        parts.Clear();

        for (int i = 0; i < waveCount; i++)
        {
            var waveImage = Instantiate(wavePrefabs[waveSpawnIndex], waveParrent.transform);
            waveImage.type = Image.Type.Filled;
            waveImage.fillMethod = Image.FillMethod.Horizontal;
            waveImage.fillOrigin = (waveSpawnIndex % 2 == 0) ? 1 : 0;
            parts.Add(waveImage);

            waveSpawnIndex++;
            if (waveSpawnIndex >= wavePrefabs.Count) waveSpawnIndex = 0;
        }
    }
    #endregion
    #region ShowAndHideMethod
    public void Hide(Action callback)
    {
        StopAllCoroutines();
        StartCoroutine(RandomFadeRoutine(0, Help));
        void Help()
        {
            callback?.Invoke();
        }
    }
    public void HideImmediately()
    {
        StopAllCoroutines();
        foreach (var item in parts)
            item.fillAmount = 0;
        loadingText.alpha = 0;
    }
    public void Show(Action callback)
    {
        StopAllCoroutines();
        StartCoroutine(RandomFadeRoutine(1, Help));
        void Help()
        {
            callback?.Invoke();
        }
    }
    public void ShowImmediately()
    {
        gameObject.SetActive();
        StopAllCoroutines();
        foreach (var item in parts)
            item.fillAmount = 1;
        loadingText.alpha = 1;
    }
    #endregion
    #region Animation
    IEnumerator RandomFadeRoutine(float targetFill, Action callback)
    {
        foreach (var item in parts)
            item.DOFillAmount(targetFill, animationTime).SetEase(Ease.Linear);

        loadingText.DOFade(targetFill, animationTime);

        yield return new WaitForSeconds(animationTime);
        callback?.Invoke();
    }
    #endregion
}
