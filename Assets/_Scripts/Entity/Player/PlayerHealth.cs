using System;
using UnityEngine;

namespace SGGames.Scripts.Entities
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private int m_initialHealth;
        [SerializeField] private int m_currentHealth;
        [SerializeField] private int m_maxHealth;

        private void Start()
        {
             m_maxHealth = m_initialHealth;
             m_currentHealth = m_maxHealth;
        }

        public bool CanTakeDamage()
        {
            return m_currentHealth > 0;
        }
        public void TakeDamage(int damage)
        {
            if (m_currentHealth <= 0) return;
            m_currentHealth -= damage;
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
