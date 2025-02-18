using System;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.Entities
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private int m_currentHealth;
        [SerializeField] private IntEvent m_updateWeaponHealthEvent;
        
        private PlayerHealth m_playerHealth;
        private readonly int C_MAX_WEAPON_HEALTH = 3;
        
        private void Start()
        {
            m_currentHealth = C_MAX_WEAPON_HEALTH;
            m_playerHealth = GetComponent<PlayerHealth>();
            m_updateWeaponHealthEvent.Raise(m_currentHealth);
        }

        public void TakeDamage(int damage)
        {
            if (m_currentHealth <= 0 && m_playerHealth.CanTakeDamage())
            {
                m_playerHealth.TakeDamage(damage);
                return;
            }
            m_currentHealth -= damage;
            m_updateWeaponHealthEvent.Raise(m_currentHealth);
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
