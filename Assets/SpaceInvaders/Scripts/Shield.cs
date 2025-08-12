using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace SpaceInvaders
{
    [RequireComponent(typeof(Tilemap))]
    public class Shield : MonoBehaviour
    {
        private Tilemap _tilemap;

        private void Awake() => _tilemap = GetComponent<Tilemap>();

        public void DestroyShieldTile(Vector3 position)
        {
            var cell = FindNearestOccupiedTile(_tilemap.WorldToCell(position));
            _tilemap.SetTile(cell, null);
        }

        private Vector3Int FindNearestOccupiedTile(Vector3Int startPosition)
        {
            Queue<Vector3Int> queue = new Queue<Vector3Int>();
            HashSet<Vector3Int> visited = new HashSet<Vector3Int>();

            queue.Enqueue(startPosition);
            visited.Add(startPosition);

            int maxSearchRadius = 10;
            int currentRadius = 0;

            while (queue.Count > 0 && currentRadius < maxSearchRadius)
            {
                int levelSize = queue.Count;
                for (int i = 0; i < levelSize; i++)
                {
                    Vector3Int currentPosition = queue.Dequeue();

                    if (_tilemap.HasTile(currentPosition))
                    {
                        return currentPosition;
                    }

                    Vector3Int[] neighbors = {
                        currentPosition + new Vector3Int(0, -1, 0),
                        currentPosition + new Vector3Int(0, 1, 0),
                        currentPosition + new Vector3Int(1, 0, 0),
                        currentPosition + new Vector3Int(-1, 0, 0),
                    };

                    foreach (Vector3Int neighbor in neighbors)
                    {
                        if (visited.Contains(neighbor)) continue;

                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
                currentRadius++;
            }

            return Vector3Int.zero;
        }
    }
}