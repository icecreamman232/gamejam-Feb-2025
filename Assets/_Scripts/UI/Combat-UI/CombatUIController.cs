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
        [SerializeField] private PreCombatUIView preCombatUIView;

        private readonly int C_DEFAULT_MOMENT_NUMBER = 3;
        
        
        private void Start()
        {
            preCombatUIView.HideView();
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
