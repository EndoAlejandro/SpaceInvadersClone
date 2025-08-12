using UnityEngine;

namespace SpaceInvaders.Enemies
{
    /// <summary>
    /// Each enemy has his own Score and Sprite.
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyStats", menuName = "Space Invaders/Enemy Stats", order = 1)]
    public class EnemyStatsSo : ScriptableObject
    {
        [field: SerializeField] public int Points { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}