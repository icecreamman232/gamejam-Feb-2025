using System.Collections.Generic;
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
        
        private List<TileController> m_tileList;
        
        private readonly float C_CENTER_OFFSET_Y = 0.5f;
        private readonly float C_CENTER_OFFSET_X = 0.5f;

        private void Start()
        {
            Initialize();
        }

        #region Room Creation
        private void Initialize()
        {
            CreateRoomLayout(m_tilePrefab,m_widthSize, m_heightSize);
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
                    m_tileList.Add(newTile);
                }  
            }
        }
        #endregion
        
    }
}
