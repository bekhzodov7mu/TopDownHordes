using UnityEngine;

namespace TopDownHordes.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(PlayerSpellsContainer), menuName = "GamePlay SO/" + nameof(PlayerSpellsContainer))]
    public class PlayerSpellsContainer : ScriptableObject
    {
        [SerializeField] private PlayerSpell[] PlayerSpells;
        
        public PlayerSpell[] GetPlayerSpells()
        {
            return PlayerSpells;
        }
    }
}