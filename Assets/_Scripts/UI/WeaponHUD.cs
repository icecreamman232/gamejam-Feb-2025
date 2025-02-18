using System;
using SGGames.Scripts.Events;
using UnityEngine;
using UnityEngine.UI;

namespace SGGames.Scripts.UI
{
    public class WeaponHUD : MonoBehaviour
    {
        [SerializeField] private Image m_icon;
        [SerializeField] private Sprite[] m_healthSprite;
        [SerializeField] private IntEvent m_updateWeaponHealthEvent;

        private void Awake()
        {
            m_updateWeaponHealthEvent.AddListener(OnUpdateWeaponHealth);
        }

        private void OnDestroy()
        {
            m_updateWeaponHealthEvent.RemoveListener(OnUpdateWeaponHealth);
        }


        private void OnUpdateWeaponHealth(int currentHealth)
        {
            if (currentHealth < 0) return; 
            m_icon.sprite = m_healthSprite[currentHealth];
        }
    }
}
