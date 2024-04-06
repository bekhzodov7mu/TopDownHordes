using System.Collections.Generic;
using Sirenix.OdinInspector;
using TopDownHordes.Projectile;
using TopDownHordes.ScriptableObjects;
using TopDownHordes.UI;
using UnityEngine;

namespace TopDownHordes.Player
{
    public class PlayerSpellsController : MonoBehaviour
    {
        [Header("Scriptable Objects")]
        [Required]
        [SerializeField] private PlayerSpellsContainer _spellsContainer;

        [Header("Scripts")]
        [SerializeField] private SpellReloadTimers _spellReloadTimers;
        
        private readonly Dictionary<int, ReloadCooldown> _reloadCooldownTimers = new();

        private PlayerSpell[] _allSpells;
        private int _activeSpellIndex;
        
        public PlayerSpell ActiveSpell { get; private set; }
        
        private void Awake()
        {
            _allSpells = _spellsContainer.GetPlayerSpells();
        }

        private void Start()
        {
            ActiveSpell = _allSpells[0];
            _activeSpellIndex = 0;
        }
        
        private void Update()
        {
            HandleSpellSwitch();
        }

        private void OnDestroy()
        {
            foreach (var reloadCooldown in _reloadCooldownTimers.Values)
            {
                reloadCooldown.Reset();
            }
            
            _reloadCooldownTimers.Clear();
        }

        private void HandleSpellSwitch()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _activeSpellIndex = (_activeSpellIndex - 1 + _allSpells.Length) % _allSpells.Length;
                ActiveSpell = _allSpells[_activeSpellIndex];

                _spellReloadTimers.SetActiveSpell(_activeSpellIndex);
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                _activeSpellIndex = (_activeSpellIndex + 1) % _allSpells.Length;
                ActiveSpell = _allSpells[_activeSpellIndex];
                
                _spellReloadTimers.SetActiveSpell(_activeSpellIndex);
            }
        }
        
        public void OnTimerValueChange(int spellIndex, float timerValue)
        {
            _spellReloadTimers.ChangeTimerValue(spellIndex, timerValue);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void OnTimerComplete(int spellIndex)
        {
            if (!_reloadCooldownTimers.Remove(spellIndex))
            {
                Debug.LogError($"There is no cooldown timer for spell with {spellIndex} index");
            }
        }

        public bool IsSpellOnCooldown()
        {
            return _reloadCooldownTimers.ContainsKey(_activeSpellIndex);
        }

        public void ReloadSpell()
        {
            _reloadCooldownTimers.Add(_activeSpellIndex, new ReloadCooldown(ActiveSpell.ReloadSpeed, _activeSpellIndex, this));
        }
    }
}