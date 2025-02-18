using System;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public enum CombatState
    {
        Start,
        Preparing,
        Combat,
        Closed,
    }
    
    public class CombatUIController : MonoBehaviour
    {
        [SerializeField] private CombatState m_combatState = CombatState.Closed;
        [SerializeField] private CanvasGroup m_combatCanvasGroup;
        [SerializeField] private PreCombatUIView preCombatUIView;

        private readonly int C_DEFAULT_MOMENT_NUMBER = 3;
        
        
        private void Start()
        {
            m_combatCanvasGroup.alpha = 0;
            m_combatCanvasGroup.blocksRaycasts = false;
            m_combatCanvasGroup.interactable = false;
            preCombatUIView.ChangeToCombatCallback += ChangeToCombatAction;
        }

        private void OnDestroy()
        {
            preCombatUIView.ChangeToCombatCallback -= ChangeToCombatAction;
        }

        private void Update()
        {
            switch (m_combatState)
            {
                case CombatState.Start:
                    StartCombatState();
                    break;
                case CombatState.Preparing:
                    PrepareCombatState();
                    break;
                case CombatState.Combat:
                    break;
                case CombatState.Closed:
                    break;
            }
        }

        private void StartCombatState()
        {
            m_combatCanvasGroup.alpha = 1;
            m_combatCanvasGroup.blocksRaycasts = true;
            m_combatCanvasGroup.interactable = true;
            
            preCombatUIView.CreateView(5);
            preCombatUIView.ShowView();

            m_combatState = CombatState.Preparing;
        }

        private void PrepareCombatState()
        {
            
        }

        private void ChangeToCombatAction()
        {
            SetCombatState(CombatState.Combat);
        }
        

        private void CloseCanvas()
        {
            preCombatUIView.HideView();  
        }

        public void SetCombatState(CombatState combatState)
        {
            m_combatState = combatState;
        }
    }
}
