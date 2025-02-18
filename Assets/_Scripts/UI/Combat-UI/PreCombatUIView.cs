using System;
using System.Collections.Generic;
using SGGames.Scripts.Abilities;
using SGGames.Scripts.Core;
using SGGames.SGGames.Scripts.Abilities;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class PreCombatUIView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        [Header("Moments")]
        [SerializeField] private CombatMomentUIController m_momentControllerPrefab;
        [SerializeField] private Transform m_momentParentPivot;
        [SerializeField] private List<CombatMomentUIController> m_momentControllerList;
        [Header("Player")]
        [SerializeField] private PlayerAbilityUIController m_playerAbilityUIPrefab;
        [SerializeField] private Transform m_playerAbilityParentPivot;
        [SerializeField] private List<PlayerAbilityUIController> m_playerAbilityUIList;
        
        private int m_currentMomentIndex;

        public Action ChangeToCombatCallback;
        
        public void CreateView(int momentNumber)
        {
            for (int i = 0; i < momentNumber; i++)
            {
                var moment  = Instantiate(m_momentControllerPrefab, m_momentParentPivot);
                moment.name = $"Moment_{i}";
                m_momentControllerList.Add(moment);
            }

            m_currentMomentIndex = 0;

            FillPlayerAbility(ServiceLocator.GetService<PlayerAbilityManager>().PlayerAbilityList);
        }
        
        public void ShowView()
        {
            m_canvasGroup.alpha = 1;
            m_canvasGroup.interactable = true;
            m_canvasGroup.blocksRaycasts = true;
        }

        public void HideView()
        {
            m_canvasGroup.alpha = 0;
            m_canvasGroup.interactable = false;
            m_canvasGroup.blocksRaycasts = false;

            foreach (var abilityUI in m_playerAbilityUIList)
            {
                abilityUI.OnAbilitySelected -= OnSendingAbilityToMomentArray;
            }
        }
        
        
        private void FillPlayerAbility(List<BaseAbility> playerAbilities)
        {
            for (int i = 0; i < playerAbilities.Count; i++)
            {
                var ability  = Instantiate(m_playerAbilityUIPrefab, m_playerAbilityParentPivot);
                ability.name = $"PlayerAbility_{i}";
                ability.SetView(playerAbilities[i]);
                m_playerAbilityUIList.Add(ability);
                m_playerAbilityUIList[i].OnAbilitySelected += OnSendingAbilityToMomentArray;
            }
        }

        private void OnSendingAbilityToMomentArray(BaseAbility ability)
        {
            if (m_currentMomentIndex >= m_momentControllerList.Count) return;
            m_momentControllerList[m_currentMomentIndex].SetMoment(ability.Data.Icon);
            m_currentMomentIndex++;
            if (m_currentMomentIndex >= m_momentControllerList.Count)
            {
                m_canvasGroup.alpha = 0;
                m_canvasGroup.interactable = false;
                m_canvasGroup.blocksRaycasts = false;
                ChangeToCombatCallback?.Invoke();
            }
        }

        public void OnPressStartCombatButton()
        {
            m_canvasGroup.alpha = 0;
            m_canvasGroup.interactable = false;
            m_canvasGroup.blocksRaycasts = false;
            ChangeToCombatCallback?.Invoke();
        }
    }
}
