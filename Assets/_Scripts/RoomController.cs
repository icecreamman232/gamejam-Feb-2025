using System.Collections.Generic;
using SGGames.Scripts.Entities;
using SGGames.Scripts.Tilesets;
using UnityEngine;

namespace SGGames.Scripts.World
{
    public class RoomController : MonoBehaviour
    {
        [SerializeField] private TileController m_tilePrefab;
        [SerializeField] private int m_widthSize;
        [SerializeField] private int m_heightSize;
        [SerializeField] private Vector2 m_playerSpawnPosition;
        [SerializeField] private GameObject m_playerPrefab;
        
        private List<TileController> m_tileList;
        
        private readonly float C_CENTER_OFFSET_Y = 0.5f;
        private readonly float C_CENTER_OFFSET_X = 0.5f;

        private void Start()
        {
            Initialize();
        }

        public Vector2 TileToWorldPosition(Vector2 tilePosition)
        {
            return m_tileList[(int)(tilePosition.y * m_heightSize + tilePosition.x)].transform.position;
        }

        #region Room Creation
        private void Initialize()
        {
            CreateRoomLayout(m_tilePrefab,m_widthSize, m_heightSize);
            CreatePlayer(m_playerPrefab, m_playerSpawnPosition);
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

        private void CreatePlayer(GameObject playerPrefab, Vector2 spawnPos)
        {
            var player = Instantiate(playerPrefab);
            player.transform.position = m_tileList[(int)(spawnPos.y * m_heightSize + spawnPos.x)].transform.position;
            var movement = player.GetComponent<PlayerMovement>();
            movement.Initialize(this,spawnPos,m_widthSize,m_heightSize);
        }
        #endregion
        
    }
}
