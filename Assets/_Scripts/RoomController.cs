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
        [SerializeField] private Vector2Int m_playerSpawnPosition;
        [SerializeField] private GameObject m_playerPrefab;
        [SerializeField] private GameObject m_stairPrefab;
        
        private List<TileController> m_tileList;
        private List<Vector2Int> m_trapSpotPositionList;
        
        private readonly float C_CENTER_OFFSET_Y = 0.5f;
        private readonly float C_CENTER_OFFSET_X = 0.5f;
        private readonly int C_TRAP_NUM_MAX = 10;
        
        private PlayerMovement m_playerMovement;
        private PlayerWeapon m_playerWeapon;

        private void Start()
        {
            Initialize();
        }
        
        public Vector2 TileToWorldPosition(Vector2Int tilePosition)
        {
            return GetTileAtTilePosition(tilePosition).transform.position;
        }

        public Vector2 TileToWorldPosition(int x, int y)
        {
            return GetTileAtTilePosition(new Vector2Int(x,y)).transform.position;
        }

        public TileController GetTileAtTilePosition(Vector2Int tilePosition)
        {
            return m_tileList[(tilePosition.y * m_heightSize + tilePosition.x)];
        }

        #region Room Creation
        private void Initialize()
        {
            CreateRoomLayout(m_tilePrefab,m_widthSize, m_heightSize);
            CreateStair();
            CreatePlayer(m_playerPrefab, m_playerSpawnPosition);
            FillTrapSpot(C_TRAP_NUM_MAX);
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
                    newTile.Initialize(this,new Vector2Int(i,j));
                    newTile.name = $"Tile x{i}-y{j}";
                    m_tileList.Add(newTile);
                }  
            }
        }

        private void CreatePlayer(GameObject playerPrefab, Vector2Int spawnPos)
        {
            var player = Instantiate(playerPrefab);
            player.transform.position = m_tileList[(int)(spawnPos.y * m_heightSize + spawnPos.x)].transform.position;
            m_playerMovement = player.GetComponent<PlayerMovement>();
            m_playerMovement.Initialize(this,spawnPos,m_widthSize,m_heightSize);
            m_playerMovement.OnPlayerFinishedMoving += OnPlayerFinishedMoving;

            m_playerWeapon = player.GetComponent<PlayerWeapon>();
        }

        private void FillTrapSpot(int maxEnemy)
        {
            m_trapSpotPositionList = new List<Vector2Int>();

            for (int i = 0; i < maxEnemy; i++)
            {
                var x = Random.Range(0, m_widthSize);
                var y = Random.Range(0, m_widthSize);
                m_trapSpotPositionList.Add(new Vector2Int(x, y));
            }
        }

        private void CreateStair()
        {
            var stair = Instantiate(m_stairPrefab,TileToWorldPosition(m_widthSize-1,m_heightSize-1),Quaternion.identity,transform);
        }
        #endregion

        private bool HasTrapSpot(Vector2Int tilePosition)
        {
            return m_trapSpotPositionList.Contains(tilePosition);
        }

        private void RemoveTrapAt(Vector2Int tilePosition)
        {
            m_trapSpotPositionList.Remove(tilePosition);
        }
        
        private void OnPlayerFinishedMoving(Vector2Int playerPosition)
        {
            var trapPos = Vector2Int.zero;
            foreach (var spot in m_trapSpotPositionList)
            {
                if (spot == playerPosition)
                {
                    trapPos = spot;
                    Debug.Log("There's a trap!");
                    m_playerWeapon.TakeDamage(1);
                }
            }
            RemoveTrapAt(trapPos);
        }

        public void OnCheckTile(Vector2Int tilePosition)
        {
            if (HasTrapSpot(tilePosition))
            {
                RemoveTrapAt(tilePosition);
                Debug.Log("There's a trap!");
                m_playerWeapon.TakeDamage(1);
            }
        }
        
        public bool IsInPlayerRange(Vector2Int tilePosition)
        {
            var playerPos = m_playerMovement.TilePosition;
            var isInHorizontally = Mathf.Abs(tilePosition.x - playerPos.x) < 2;
            var isInVertically = Mathf.Abs(tilePosition.y - playerPos.y) < 2;
            return isInHorizontally && isInVertically;
        }
    }
}
