using System.Collections.Generic;
using UnityEngine;

namespace SpaceInvaders.Enemies
{
    /// <summary>
    /// Spawn and move all enemies.
    /// </summary>
    public class EnemiesController : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _spawner;

        public static List<Enemy> Enemies { get; private set; }

        public static int EnemiesAmount { get; private set; }

        private void Awake()
        {
            Enemies = new List<Enemy>();
            BaseEnemy.OnDeath += EnemyOnDeath;
        }

        // Create the enemies from a pool.
        private void Start()
        {
            var se = _spawner.SpawnSpecialEnemy();
            Enemies = _spawner.SpawnBaseEnemies();
            EnemiesAmount = Enemies.Count;
        }

        // When an enemy dies is removed from the enemy collection.
        private void EnemyOnDeath(BaseEnemy baseEnemy)
        {
            if (baseEnemy is not Enemy enemy) return;

            Enemies?.Remove(enemy);
            if (Enemies?.Count == 0)
            {
                GameManager.WinGame();
            }
        }

        private void OnDestroy() => BaseEnemy.OnDeath -= EnemyOnDeath;
    }
}