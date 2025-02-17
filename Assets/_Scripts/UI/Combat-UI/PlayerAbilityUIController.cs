using System;
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
            m_attackCounterText.text = ability.Data.AttackCount.ToString();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (m_cacheAbilityData.CurrentState == AbilityState.COOLDOWN) return;
            m_cacheAbilityData.DoAttack();
            m_attackCounterText.text = m_cacheAbilityData.AttackRemaining.ToString();
            OnAbilitySelected?.Invoke(m_cacheAbilityData);
        }
    }
}
