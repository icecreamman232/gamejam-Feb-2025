using System;
using UnityEngine;

namespace SGGames.Scripts.Entities
{
    public class PlayerMovement : EntityMovement
    {
        public Action<Vector2Int> OnPlayerFinishedMoving;
        
        protected override void Update()
        {
            UpdateInput();
            base.Update();
        }

        private void UpdateInput()
        {
            if (m_movementState == EntityMovementState.Moving) return;

            if (Input.GetKeyDown(KeyCode.W))
            {
                m_movementDirection = MovementDirection.UP;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                m_movementDirection = MovementDirection.DOWN;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                m_movementDirection = MovementDirection.LEFT;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                m_movementDirection = MovementDirection.RIGHT;
            }
            

            Vector2Int tileDest = Vector2Int.zero;
            switch (m_movementDirection)
            {
                case MovementDirection.LEFT:
                    tileDest = m_tileCurrentPosition + Vector2Int.left;
                    MoveTo(tileDest);
                    break;
                case MovementDirection.RIGHT:
                    tileDest = m_tileCurrentPosition + Vector2Int.right;
                    MoveTo(tileDest);
                    break;
                case MovementDirection.UP:
                    tileDest = m_tileCurrentPosition + Vector2Int.up;
                    MoveTo(tileDest);
                    break;
                case MovementDirection.DOWN:
                    tileDest = m_tileCurrentPosition + Vector2Int.down;
                    MoveTo(tileDest);
                    break;
            }
        }

        protected override void FinishMovement()
        {
            base.FinishMovement();
            OnPlayerFinishedMoving?.Invoke(m_tileCurrentPosition);
        }
    }
}
