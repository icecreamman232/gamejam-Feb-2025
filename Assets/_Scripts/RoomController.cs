using System;
using System.Collections.Generic;
using SGGames.Scripts.Core;
using SGGames.Scripts.Entities;
using SGGames.Scripts.Events;
using SGGames.Scripts.Managers;
using SGGames.Scripts.Tilesets;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SGGames.Scripts.World
{
    public class RoomController : MonoBehaviour
    {
        [SerializeField] private TileController m_tilePrefab;
        [SerializeField] private int m_widthSize;
        [SerializeField] private int m_heightSize;
        [SerializeField] private Vector2Int m_playerSpawnPosition;
        [SerializeField] private GameObject m_playerPrefab;
        [SerializeField] private GameEvents m_gameEvents;
        
        private List<TileController> m_tileList;
        private List<Vector2Int> m_enemySpotPositionList;
        
        private readonly float C_CENTER_OFFSET_Y = 0.5f;
        private readonly float C_CENTER_OFFSET_X = 0.5f;
        private readonly int C_ENEMY_NUM_MAX = 10;

        public Action OnStepOnEnemySpot;

        private void Start()
        {
            Initialize();
        }

        public Vector2 TileToWorldPosition(Vector2 tilePosition)
        {
            return GetTileAtTilePosition(tilePosition).transform.position;
        }

        public TileController GetTileAtTilePosition(Vector2 tilePosition)
        {
            return m_tileList[(int)(tilePosition.y * m_heightSize + tilePosition.x)];
        }

        #region Room Creation
        private void Initialize()
        {
            CreateRoomLayout(m_tilePrefab,m_widthSize, m_heightSize);
            CreatePlayer(m_playerPrefab, m_playerSpawnPosition);
            FillEnemySpot(C_ENEMY_NUM_MAX);
        }

        private void CreateRoomLayout(TileController tilePrefab, int width, int height)
        {
            if (m_tileList == null)
            {
                m_tileList = new List<TileController>();
            }
            var offsetX = width / 2;
            var offsetY = height / 2;

            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    var newTile = Instantiate(tilePrefab,
                        new Vector3(i- offsetX + C_CENTER_OFFSET_X,j - offsetY + C_CENTER_OFFSET_Y,0), 
                        Quaternion.identity,transform);
                    newTile.name = $"Tile x{i}-y{j}";
                    m_tileList.Add(newTile);
                }  
            }
        }

        private void CreatePlayer(GameObject playerPrefab, Vector2Int spawnPos)
        {
            var player = Instantiate(playerPrefab);
            player.transform.position = m_tileList[(int)(spawnPos.y * m_heightSize + spawnPos.x)].transform.position;
            var movement = player.GetComponent<PlayerMovement>();
            movement.Initialize(this,spawnPos,m_widthSize,m_heightSize);
            movement.OnPlayerFinishedMoving += OnPlayerFinishedMoving;
        }

        private void FillEnemySpot(int maxEnemy)
        {
            m_enemySpotPositionList = new List<Vector2Int>();

            for (int i = 0; i < maxEnemy; i++)
            {
                var x = Random.Range(0, m_widthSize);
                var y = Random.Range(0, m_widthSize);
                m_enemySpotPositionList.Add(new Vector2Int(x, y));
            }
        }
        #endregion


        private void OnPlayerFinishedMoving(Vector2Int playerPosition)
        {
            foreach (var spot in m_enemySpotPositionList)
            {
                if (spot == playerPosition)
                {
                    Debug.Log("Step on enemy! Starting combat...");
                    OnStepOnEnemySpot?.Invoke();
                    m_gameEvents.Raise(GameEventsType.START_COMBAT);
                }
            }
        }
    }
}
