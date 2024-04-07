using UnityEngine;

namespace TopDownHordes.Enemies
{
    public class EnemyLinker : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _enemyHealth;
        [SerializeField] private BaseEnemyMovement _baseEnemyMovement;

        public EnemyHealth EnemyHealth => _enemyHealth;
        public BaseEnemyMovement EnemyMovement => _baseEnemyMovement;
    }
}