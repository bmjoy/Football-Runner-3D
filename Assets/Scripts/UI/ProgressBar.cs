using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TFPlay.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image bar;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private string format = "{0} %";
        [SerializeField] private float fillDuration = 0.25f;

        public void ResetProgress()
        {
            SetProgress(0);
        }

        public void SetProgress(float progress)
        {
            bar.DOFillAmount(progress, fillDuration);
            text.text = string.Format(format, 100 * progress);
        }

        public void SetProgress(int current, int count)
        {
            float progress = (float)current / count;
            bar.DOFillAmount(progress, fillDuration);
            text.text = string.Format(format, current, count);
        }
    }
}