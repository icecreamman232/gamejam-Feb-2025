using System.Collections.Generic;
using SGGames.Scripts.Core;
using SGGames.SGGames.Scripts.Abilities;
using UnityEngine;

namespace SGGames.Scripts.Abilities
{
    public class PlayerAbilityManager : MonoBehaviour, IGameService
    {
        [SerializeField] private AbilityData m_firstDefaultAbility;
        [SerializeField] private AbilityData m_secondDefaultAbility;
        [SerializeField] private List<BaseAbility> m_playerAbilityList;
        
        private readonly int C_PLAYER_MAX_ABILITIES = 4;
        
        public List<BaseAbility> PlayerAbilityList => m_playerAbilityList;
        
        private void Awake()
        {
            ServiceLocator.RegisterService<PlayerAbilityManager>(this);
        }

        private void Start()
        {
            m_playerAbilityList = new List<BaseAbility>();
            m_playerAbilityList.Add(new BaseAbility(m_firstDefaultAbility));
            m_playerAbilityList.Add(new BaseAbility(m_secondDefaultAbility));
        }
    }
}
