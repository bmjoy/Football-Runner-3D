using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TFPlay.UI
{
    [RequireComponent(typeof(Image))]
    public class SceneFaderUI : MonoBehaviour
    {
        [SerializeField] private float fadeTime = 0.5f;

        private Image fadeImage;

        private void Awake()
        {
            fadeImage = GetComponent<Image>();
        }

        private void Start()
        {
            FadeSilent();
            GameC.Instance.OnLevelStartLoading += OnLevelStartLoading;
            GameC.Instance.OnLevelLoaded += OnLevelLoaded;
        }

        private void OnLevelStartLoading(int levelNumber)
        {
            FadeSilent();
        }

        private void OnLevelLoaded(int levelNumber)
        {
            FadeOut();
        }

        private void FadeSilent()
        {
            fadeImage.color = fadeImage.color.WithAlpha(1);
        }

        private void FadeOut()
        {
            fadeImage.DOFade(0f, fadeTime);
        }
    }
}