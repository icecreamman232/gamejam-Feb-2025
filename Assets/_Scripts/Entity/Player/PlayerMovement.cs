using UnityEngine;

namespace SGGames.Scripts.Entities
{
    public class PlayerMovement : EntityMovement
    {
        protected override void Update()
        {
            UpdateInput();
            base.Update();
        }

        private void UpdateInput()
        {
            if (m_movementState == EntityMovementState.Moving) return;

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                m_movementDirection = MovementDirection.UP;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                m_movementDirection = MovementDirection.DOWN;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                m_movementDirection = MovementDirection.LEFT;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                m_movementDirection = MovementDirection.RIGHT;
            }
            

            var tileDest = Vector2.zero;
            switch (m_movementDirection)
            {
                case MovementDirection.LEFT:
                    tileDest = m_tileCurrentPosition + Vector2.left;
                    MoveTo(tileDest);
                    break;
                case MovementDirection.RIGHT:
                    tileDest = m_tileCurrentPosition + Vector2.right;
                    MoveTo(tileDest);
                    break;
                case MovementDirection.UP:
                    tileDest = m_tileCurrentPosition + Vector2.up;
                    MoveTo(tileDest);
                    break;
                case MovementDirection.DOWN:
                    tileDest = m_tileCurrentPosition + Vector2.down;
                    MoveTo(tileDest);
                    break;
            }
        }
    }
}
