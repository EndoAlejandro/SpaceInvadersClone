using UnityEngine;
using UnityEngine.Tilemaps;

namespace SpaceInvaders
{
    [RequireComponent(typeof(Tilemap))]
    public class Shield : MonoBehaviour
    {
        private Tilemap _tilemap;

        private void Awake() => _tilemap = GetComponent<Tilemap>();

        public void DestroyShieldTile(Collider2D projectileCollider, bool isFalling = false)
        {
            var cell = new Vector3Int();
            
            cell = _tilemap.WorldToCell(new Vector2(projectileCollider.transform.position.x + projectileCollider.bounds.size.x / 2f, projectileCollider.transform.position.y));
            _tilemap.SetTile(cell, null);
            cell = _tilemap.WorldToCell(new Vector2(projectileCollider.transform.position.x - projectileCollider.bounds.size.x / 2f, projectileCollider.transform.position.y));
            _tilemap.SetTile(cell, null);
        }
    }
}