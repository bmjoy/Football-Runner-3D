using TMPro;
using UnityEngine;

namespace TFPlay.UI
{
    public class CoinsUI : BaseUIBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinsText;

        protected override void Init()
        {
            base.Init();
            SLS.Data.Game.Coins.OnValueChanged += SetCoinsText;
            SetCoinsText(SLS.Data.Game.Coins.Value);
        }

        private void SetCoinsText(int coinsCount)
        {
            coinsText.text = coinsCount.ToString();
        }
    }
}