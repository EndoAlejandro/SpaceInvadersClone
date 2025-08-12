using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Enemies
{
    /// <summary>
    /// Manage all enemies at the same time.
    /// </summary>
    public class EnemyManager : MonoBehaviour
    {
        /// <summary>
        /// Instanced enemies.
        /// </summary>
        public static List<StandardEnemy> Enemies { get; private set; }

        public static int EnemiesAmount { get; private set; }

        [SerializeField] private EnemySpawner _spawner;
        [SerializeField] private float _specialEnemyCooldown = 5f;

        private float _specialEnemyTimer;

        private void Awake()
        {
            Enemies = new List<StandardEnemy>();
            BaseEnemy.OnDeath += EnemyOnDeath;
        }

        private void Start()
        {
            // Sets special enemy timer.
            _specialEnemyTimer = _specialEnemyCooldown;
            
            // Spawn initial enemies.
            Enemies = _spawner.SpawnBaseEnemies();
            EnemiesAmount = Enemies.Count;
        }

        /// <summary>
        /// Timer for special enemy spawn.
        /// </summary>
        private void Update()
        {
            _specialEnemyTimer -= Time.deltaTime;
            if (_specialEnemyTimer > 0f) return;
            
            _specialEnemyTimer = _specialEnemyCooldown;
            _spawner.SpawnSpecialEnemy();
        }

        // When an enemy dies is removed from the enemy collection.
        private void EnemyOnDeath(BaseEnemy baseEnemy)
        {
            if (baseEnemy is not StandardEnemy enemy) return;

            Enemies?.Remove(enemy);
            if (Enemies?.Count == 0)
            {
                GameManager.WinGame();
            }
        }

        private void OnDestroy() => BaseEnemy.OnDeath -= EnemyOnDeath;
    }
}