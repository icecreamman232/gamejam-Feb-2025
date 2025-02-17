using System;
using System.Collections.Generic;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class CombatUIController : MonoBehaviour
    {
        [SerializeField] private CombatUIView m_combatUIView;
        [Header("Moments")]
        [SerializeField] private CombatMomentUIController m_momentControllerPrefab;
        [SerializeField] private Transform m_momentParentPivot;
        [SerializeField] private List<CombatMomentUIController> m_momentControllerList;

        private readonly int C_DEFAULT_MOMENT_NUMBER = 3;

        private void Start()
        {
            m_combatUIView.HideView();
            
            for (int i = 0; i < C_DEFAULT_MOMENT_NUMBER; i++)
            {
                var moment  = Instantiate(m_momentControllerPrefab, m_momentParentPivot);
                moment.name = $"Moment_{i}";
                m_momentControllerList.Add(moment);
            }
        }

        public void OpenCanvas()
        {
            m_combatUIView.ShowView();
        }

        private void CloseCanvas()
        {
            m_combatUIView.HideView();  
        }
    }
}
