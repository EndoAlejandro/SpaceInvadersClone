using UnityEngine;

namespace SpaceInvaders.Player
{
    [CreateAssetMenu(fileName = "PlayerStats", menuName = "Space Invaders/Player Stats", order = 1)]
    public class PlayerStatsSo : ScriptableObject
    {
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public int LivesAmount { get; private set; }
    }
}