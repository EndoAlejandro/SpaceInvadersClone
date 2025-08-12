using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace SpaceInvaders
{
    /// <summary>
    /// Shield built using Tilemaps.
    /// </summary>
    [RequireComponent(typeof(Tilemap))]
    public class Shield : MonoBehaviour
    {
        [SerializeField] private int _maxSearchRadius = 5;

        private Tilemap _tilemap;

        private void Awake() => _tilemap = GetComponent<Tilemap>();

        /// <summary>
        /// Finds the nearest tile and destroy it.
        /// </summary>
        /// <param name="position"></param>
        public void DestroyShieldTile(Vector3 position)
        {
            var cell = FindNearestOccupiedTile(_tilemap.WorldToCell(position));
            _tilemap.SetTile(cell, null);
        }

        /// <summary>
        /// Do a quick search around the hit point so it finds the nearest available Tile.
        /// Usually the projectile is a little behind the collision point so this search is necessary to have a consistent result.
        /// </summary>
        /// <param name="startPosition">Position of the projectile.</param>
        /// <returns>Nearest Tile or Vector3Int.zero.</returns>
        private Vector3Int FindNearestOccupiedTile(Vector3Int startPosition)
        {
            // Queue for pending neighbors
            Queue<Vector3Int> queue = new Queue<Vector3Int>();
            // Visited positions, HashSet helps to duplicated check.
            HashSet<Vector3Int> visited = new HashSet<Vector3Int>();

            // Start with the initial position.
            queue.Enqueue(startPosition);
            visited.Add(startPosition);

            int currentRadius = 0;

            // Loop until there is no more neighbors or max radius reached.
            while (queue.Count > 0 && currentRadius < _maxSearchRadius)
            {
                int levelSize = queue.Count;
                for (int i = 0; i < levelSize; i++)
                {
                    Vector3Int currentPosition = queue.Dequeue();

                    // If tile is found, return it.
                    if (_tilemap.HasTile(currentPosition))
                    {
                        return currentPosition;
                    }

                    // neighbors to check.
                    Vector3Int[] neighbors = {
                        currentPosition + new Vector3Int(0, -1, 0),
                        currentPosition + new Vector3Int(0, 1, 0),
                        currentPosition + new Vector3Int(1, 0, 0),
                        currentPosition + new Vector3Int(-1, 0, 0),
                    };

                    // Add the neighbors to the next iteration.
                    foreach (Vector3Int neighbor in neighbors)
                    {
                        if (visited.Add(neighbor))
                            queue.Enqueue(neighbor);
                    }
                }
                currentRadius++;
            }

            // Returns default value if no position was found.
            return Vector3Int.zero;
        }
    }
}