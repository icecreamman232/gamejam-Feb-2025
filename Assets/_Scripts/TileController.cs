using SGGames.Scripts.World;
using UnityEngine;

namespace SGGames.Scripts.Tilesets
{
    public class TileController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_spriteRenderer;

        private Vector2Int m_tilePosition;
        private RoomController m_roomController;
        
        public void Initialize(RoomController roomController, Vector2Int tilePosition)
        {
            m_roomController = roomController;
            m_tilePosition = tilePosition;
        }

        private void OnMouseEnter()
        {
            m_spriteRenderer.color = m_roomController.IsInPlayerRange(m_tilePosition) ? Color.green : Color.red;
        }

        private void OnMouseExit()
        {
            m_spriteRenderer.color= Color.white;
        }

        private void OnMouseDown()
        {
            m_roomController.OnCheckTile(m_tilePosition);
        }
    }
}
