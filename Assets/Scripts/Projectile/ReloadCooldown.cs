using DG.Tweening;
using TopDownHordes.Player;

namespace TopDownHordes.Projectile
{
    public class ReloadCooldown
    {
        public float TimerValue { get; private set; }

        private readonly Tween _tween;
        
        public ReloadCooldown(float reloadTime, int spellIndex, PlayerSpellsController playerStaff)
        {
            TimerValue = reloadTime;
            _tween = DOTween.To(() => reloadTime, x =>
            {
                TimerValue = x;
                playerStaff.OnTimerValueChange(spellIndex, TimerValue);
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
            TimerValue = 0;
            _tween?.Kill();
        }
    }
}