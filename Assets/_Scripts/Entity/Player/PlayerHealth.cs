using System;
using MoreMountains.Feedbacks;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Entities
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int m_initialHealth;
        [SerializeField] private int m_currentHealth;
        [SerializeField] private int m_maxHealth;
        [SerializeField] private IntEvent m_updateHealthEvent;
        [SerializeField] private MMF_Player m_hurtFeedback;

        private void Start()
        {
             m_maxHealth = m_initialHealth;
             m_currentHealth = m_maxHealth;
             m_updateHealthEvent.Raise(m_currentHealth);
        }

        public bool CanTakeDamage()
        {
            return m_currentHealth > 0;
        }
        public void TakeDamage(int damage)
        {
            if (m_currentHealth <= 0) return;
            m_currentHealth -= damage;
            m_updateHealthEvent.Raise(m_currentHealth);
            m_hurtFeedback.PlayFeedbacks();
            if (m_currentHealth <= 0)
            {
                Kill();
            }
        }

        private void Kill()
        {
            
        }
    }
}
