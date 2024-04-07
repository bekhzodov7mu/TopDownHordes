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

            var activeSpell = _spellsController.ActiveSpell;
            var projectile = Instantiate(activeSpell.ProjectilePrefab, _shootPoint.position, _shootPoint.rotation); // TODO: ObjectPool
            
            projectile.Init(activeSpell.ProjectileSpeed, activeSpell.Damage);
            
            _spellsController.ReloadSpell();
        }
    }
}