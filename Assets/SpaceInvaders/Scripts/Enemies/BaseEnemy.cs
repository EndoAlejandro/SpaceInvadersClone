using System;
using SpaceInvaders.Core;

namespace SpaceInvaders.Enemies
{
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
            GameManager.AddScore(_points);
            ReturnToPool();
        }
    }
}