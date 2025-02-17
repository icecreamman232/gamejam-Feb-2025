using System;
using SGGames.Scripts.Abilities;
using SGGames.SGGames.Scripts.Abilities;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SGGames.Scripts.UI
{
    public class PlayerAbilityUIController : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image m_abilityIcon;
        [SerializeField] private TextMeshProUGUI m_attackCounterText;

        private BaseAbility m_cacheAbilityData;
        
        public Action<BaseAbility> OnAbilitySelected;
        
        public void SetView(BaseAbility ability)
        {
            m_cacheAbilityData = ability;
            m_abilityIcon.sprite = ability.Data.Icon;
            m_attackCounterText.text =  ability.Data.AbilityType == AbilityType.Offense 
                ? ability.Data.AttackCount.ToString()
                : ability.Data.BlockCount.ToString();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (m_cacheAbilityData.CurrentState == AbilityState.COOLDOWN) return;
            if (m_cacheAbilityData.Data.AbilityType == AbilityType.Offense)
            {
                m_cacheAbilityData.DoAttack();
            }
            else
            {
                m_cacheAbilityData.DoDefense();
            }
            
            m_attackCounterText.text =  m_cacheAbilityData.Data.AbilityType == AbilityType.Offense 
                ? m_cacheAbilityData.AttackRemaining.ToString()
                : m_cacheAbilityData.BlockRemaining.ToString();
            OnAbilitySelected?.Invoke(m_cacheAbilityData);
        }
    }
}
