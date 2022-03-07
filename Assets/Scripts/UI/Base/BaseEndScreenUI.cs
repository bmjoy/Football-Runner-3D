using UnityEngine;
using DG.Tweening;

namespace TFPlay.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class BaseEndScreenUI : BaseUIBehaviour
    {
        [SerializeField] private float fadeTime = 0.5f;

        private CanvasGroup canvasGroup;

        protected override void Awake()
        {
            base.Awake();
            canvasGroup = GetComponent<CanvasGroup>();
        }

        protected override void Init()
        {
            base.Init();
            canvasGroup.alpha = 0f;
        }

        public override void Show()
        {
            base.Show();
            canvasGroup.DOFade(1f, fadeTime);
        }

        public override void Hide()
        {
            canvasGroup.DOFade(0f, fadeTime).OnComplete(() => base.Hide());
        }
    }
}