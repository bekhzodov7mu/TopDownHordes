using UnityEngine;

namespace TopDownHordes.Enemies
{
    /// <summary>
    /// Single access point to all enemy scripts
    /// </summary>
    public class EnemyLinker : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private BaseEnemyMovement _baseEnemyMovement;

        public EnemyHealth EnemyHealth => _enemyHealth;
        public BaseEnemyMovement EnemyMovement => _baseEnemyMovement;
    }
}