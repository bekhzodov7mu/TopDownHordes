using UnityEngine;

namespace TopDownHordes.Player
{
    public class PlayerStaff : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform _shootPoint;

        [SerializeField] private PlayerSpellsController _spellsController;
        
        public void Update()
        {
            Shoot();
        }

        private void Shoot()
        {
            if (!Input.GetKeyDown(KeyCode.X))
            {
                return;
            }
            
            if (_spellsController.IsSpellOnCooldown())
            {
                return;
            }

            Instantiate(_spellsController.ActiveSpell.SpellPrefab, _shootPoint.position, _shootPoint.rotation);
            _spellsController.ReloadSpell();
        }
    }
}