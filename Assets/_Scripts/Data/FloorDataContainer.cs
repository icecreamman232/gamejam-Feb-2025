using System;
using UnityEngine;

namespace SGGames.Scripts.Data
{
    [CreateAssetMenu(menuName = "SGGames/Floor Data Container")]
    public class FloorDataContainer : ScriptableObject
    {
        [SerializeField] private FloorData[] m_floorData;
        
        public FloorData GetFloorData(int floorIndex) => m_floorData[floorIndex];
    }
    [Serializable]
    public class FloorData
    {
        public int TrapNumber;
    }
}
