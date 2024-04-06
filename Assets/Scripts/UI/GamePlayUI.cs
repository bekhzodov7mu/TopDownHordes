using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownHordes.UI
{
    public class GamePlayUI: MonoBehaviour
    {
        [SerializeField] private Image _healthBarFillImage;

        private Tween _healthBarTween;
        
        public void OnPlayerHealthChange(float maxValue, float newValue)
        {
            _healthBarTween?.Kill();

            float ratio = newValue / maxValue;
            
            float prevValue = _healthBarFillImage.fillAmount;
            _healthBarTween = DOTween.To(() => prevValue, x =>
            {
                _healthBarFillImage.fillAmount = x;
            }, ratio, 0.3f);
        }
    }
}