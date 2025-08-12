using System;
using SpaceInvaders.Core;

namespace SpaceInvaders.Enemies
{
    public abstract class BaseEnemy : PoolableMonoBehaviour
    {
        public static event Action<BaseEnemy> OnDeath;

        private int _points;
        
        protected void Setup(EnemyStatsSo enemyStats)
        {
            _points = enemyStats.Points;
        }

        public void Kill()
        {
            OnDeath?.Invoke(this);
            GameManager.AddScore(_points);
            ReturnToPool();
        }
    }
}