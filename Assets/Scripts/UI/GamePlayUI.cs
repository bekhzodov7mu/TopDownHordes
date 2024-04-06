using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TopDownHordes.UI
{
    public class GamePlayUI: MonoBehaviour
    {
        [SerializeField] private Image _healthBarImage;

        private Tween _healthBarTween;
        
        public void OnPlayerHealthChange(int newValue)
        {
            _healthBarTween?.Kill();
            
            float prevValue = _healthBarImage.fillAmount;
            _healthBarTween = DOTween.To(() => prevValue, x =>
            {
                _healthBarImage.fillAmount = x;
            }, newValue, 0.3f);
        }
        
        
    }
}