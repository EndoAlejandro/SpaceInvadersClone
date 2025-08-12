using System;
using SpaceInvaders.Core;

namespace SpaceInvaders.Enemies
{
    /// <summary>
    /// Base clase for Standard and Special enemies.
    /// </summary>
    public abstract class BaseEnemy : PoolableMonoBehaviour
    {
        public static Action<BaseEnemy> OnDeath;

        protected EnemyStatsSo stats;
        
        private int _points;
        
        protected void Setup(EnemyStatsSo enemyStats)
        {
            stats = enemyStats;
            _points = enemyStats.Points;
        }

        public virtual void Kill()
        {
            OnDeath?.Invoke(this);
            // All enemies give points when killed.
            GameManager.AddScore(_points);
            ReturnToPool();
        }
    }
}