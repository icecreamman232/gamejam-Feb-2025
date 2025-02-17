using System.Collections.Generic;
using SGGames.Scripts.Core;
using UnityEngine;

namespace SGGames.Scripts.Abilities
{
    public class PlayerAbilityManager : MonoBehaviour, IGameService
    {
        [SerializeField] private AbilityData m_firstDefaultAbility;
        [SerializeField] private List<AbilityData> m_playerAbilityList;
        
        private readonly int C_PLAYER_MAX_ABILITIES = 4;
        
        private void Awake()
        {
            ServiceLocator.RegisterService<PlayerAbilityManager>(this);
        }

        private void Start()
        {
            m_playerAbilityList = new List<AbilityData>();
            m_playerAbilityList.Add(m_firstDefaultAbility);
        }
    }
}
