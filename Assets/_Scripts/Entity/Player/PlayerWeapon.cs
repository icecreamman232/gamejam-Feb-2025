using System;
using UnityEngine;

namespace SGGames.Scripts.Entities
{
    public class PlayerWeapon : MonoBehaviour
    {
        [SerializeField] private int m_currentHealth;

        private readonly int C_MAX_WEAPON_HEALTH = 3;
        
        private void Start()
        {
            m_currentHealth = C_MAX_WEAPON_HEALTH;
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
