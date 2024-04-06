using Sirenix.OdinInspector;
using TopDownHordes.ScriptableObjects;
using UnityEngine;

namespace TopDownHordes.Player
{
    public class PlayerStaff : MonoBehaviour
    {
        [Header("Scriptable Objects")]
        [Required]
        [SerializeField] private PlayerSpellsContainer _spellsContainer;
        
        [Header("Components")]
        [SerializeField] private Transform _shootPoint;

        private PlayerSpell[] _allSpells;
        private PlayerSpell _activeSpell;
        
        private int _activeSpellIndex;

        private void Awake()
        {
            _allSpells = _spellsContainer.GetPlayerSpells();
        }

        private void Start()
        {
            _activeSpell = _allSpells[0];
            _activeSpellIndex = 0;
        }
        
        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                _activeSpellIndex = (_activeSpellIndex - 1 + _allSpells.Length) % _allSpells.Length;
                _activeSpell = _allSpells[_activeSpellIndex];
            }
            else if (Input.GetKeyDown(KeyCode.W))
            {
                _activeSpellIndex = (_activeSpellIndex + 1) % _allSpells.Length;
                _activeSpell = _allSpells[_activeSpellIndex];
            }
        }
    }
}