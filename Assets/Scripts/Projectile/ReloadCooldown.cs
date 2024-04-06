using DG.Tweening;
using TopDownHordes.Player;

namespace TopDownHordes.Projectile
{
    public class ReloadCooldown
    {
        private readonly Tween _tween;
        
        private float _timerValue;
        
        public ReloadCooldown(float reloadTime, int spellIndex, PlayerSpellsController playerStaff)
        {
            _timerValue = reloadTime;
            _tween = DOTween.To(() => reloadTime, x =>
            {
                _timerValue = x;
                playerStaff.OnTimerValueChange(spellIndex, _timerValue);
            }, 0, reloadTime)
            .SetEase(Ease.Linear)
            .SetLink(playerStaff.gameObject)
            .OnComplete(()=>
            {
                playerStaff.OnTimerComplete(spellIndex);
            });
        }

        public void Reset()
        {
            _timerValue = 0;
            _tween?.Kill();
        }
    }
}