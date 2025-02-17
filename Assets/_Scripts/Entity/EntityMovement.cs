using SGGames.Scripts.World;
using UnityEngine;
using UnityEngine.Serialization;

namespace SGGames.Scripts.Entities
{

    public enum EntityMovementState
    {
        Idle,
        Moving,
    }
    
    public enum MovementDirection
    {
        NONE,
        LEFT,
        RIGHT,
        UP,
        DOWN,
    }
    
    public class EntityMovement : MonoBehaviour
    {
        [SerializeField] protected EntityMovementState m_movementState;
        [SerializeField] protected MovementDirection m_movementDirection;
        [SerializeField] protected Vector2Int m_tileCurrentPosition;
        protected readonly float C_ENTITY_SPEED = 3f;
        
        protected Vector2 m_worldDestPosition;

        protected RoomController m_roomController;
        protected int m_boundWidth;
        protected int m_boundHeight;

        public virtual void Initialize(RoomController roomController, Vector2Int tilePosition,int boundWidth, int boundHeight)
        {
            m_roomController = roomController;
            m_boundWidth = boundWidth;
            m_boundHeight = boundHeight;
            m_tileCurrentPosition = tilePosition;
            m_worldDestPosition =  m_roomController.TileToWorldPosition(tilePosition);
            m_movementState = EntityMovementState.Idle;
        }
        
        public void MoveTo(Vector2Int tilePosition)
        {
            if (m_movementState == EntityMovementState.Moving) return;
            if (tilePosition.x < 0 || tilePosition.x >= m_boundWidth || tilePosition.y < 0 ||
                tilePosition.y >= m_boundHeight)
            {
                return;
            }
            
            m_tileCurrentPosition = tilePosition;
            m_movementState = EntityMovementState.Moving;
            m_worldDestPosition = m_roomController.TileToWorldPosition(tilePosition);
        }

        protected virtual void UpdateMovement()
        {
            if (m_movementState == EntityMovementState.Idle) return;
            
            transform.position = Vector2.MoveTowards(transform.position, m_worldDestPosition, C_ENTITY_SPEED * Time.deltaTime);
            if ((Vector2)transform.position == m_worldDestPosition)
            {
                FinishMovement();
            }
        }

        protected virtual void FinishMovement()
        {
            transform.position = m_worldDestPosition;
            m_movementState = EntityMovementState.Idle;
            m_movementDirection = MovementDirection.NONE;
        }

        protected virtual void Update()
        {
            UpdateMovement();
        }
    }
}

