using System;
using MoreMountains.Feedbacks;
using SGGames.Scripts.World;
using UnityEngine;

namespace SGGames.Scripts.Entities
{
    public class PlayerMovement : EntityMovement
    {
        [SerializeField] private MMF_Player m_excalmationMarkVFX;
        public Action<Vector2Int> OnPlayerFinishedMoving;

        public override void Initialize(RoomController roomController, Vector2Int tilePosition, int boundWidth, int boundHeight)
        {
            base.Initialize(roomController, tilePosition, boundWidth, boundHeight);
            roomController.OnStepOnEnemySpot += PlayStepOnEnemySpotVFX;
        }

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

        private void PlayStepOnEnemySpotVFX()
        {
            m_excalmationMarkVFX.PlayFeedbacks();
        }
    }
}
