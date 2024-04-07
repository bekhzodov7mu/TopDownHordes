using System.Collections.Generic;
using Sirenix.OdinInspector;
using TopDownHordes.ScriptableObjects;
using UnityEngine;

namespace TopDownHordes.UI
{
    public class SpellReloadTimers : MonoBehaviour
    {
        [Header("Scriptable Objects")]
        [Required]
        [SerializeField] private PlayerSpellsContainer _spellsContainer;
        
        [Header("Prefabs")]
        [SerializeField] private ReloadTimerView _reloadTimerViewPrefab;

        [Header("Components")]
        [SerializeField] private RectTransform _timersHolder;
        
        private readonly Dictionary<int, ReloadTimerView> _reloadTimerViews = new();

        private PlayerSpell[] _playerSpells;

        public void CreateSpellReloadTimers()
        {
            _playerSpells = _spellsContainer.GetPlayerSpells();
            
            for (int i = 0; i < _playerSpells.Length; i++)
            {
                ReloadTimerView timerView = Instantiate(_reloadTimerViewPrefab, _timersHolder);
                timerView.Init(_playerSpells[i].SpellName);

                _reloadTimerViews.Add(i, timerView);
            }
        }

        public void ChangeTimerValue(int spellIndex, float timerValue)
        {
            var timerRatio = timerValue / _playerSpells[spellIndex].ReloadSpeed;
            _reloadTimerViews[spellIndex].SetTimerValue(timerRatio);
        }

        public void SetActiveSpell(int spellIndex)
        {
            foreach ((int index, ReloadTimerView reloadTimerView) in _reloadTimerViews)
            {
                reloadTimerView.SetAsActive(index == spellIndex);
            }
        }
    }
}