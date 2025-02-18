using System;
using System.Collections.Generic;
using SGGames.Scripts.Events;
using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class PlayerHealthHUD : MonoBehaviour
    {
        [SerializeField] private GameObject m_heartPrefab;
        [SerializeField] private IntEvent m_healthUpdateEvent;

        private List<GameObject> m_hearts;
        
        private void Awake()
        {
            m_hearts = new List<GameObject>();
            m_healthUpdateEvent.AddListener(OnUpdatePlayerHealth);
        }

        private void OnDestroy()
        {
            m_healthUpdateEvent.RemoveListener(OnUpdatePlayerHealth);
        }

        private void OnUpdatePlayerHealth(int currentHealth)
        {
            if (m_hearts.Count > 0)
            {
                for (int i = 0; i < m_hearts.Count; i++)
                {
                    Destroy(m_hearts[i]);
                }
            
                m_hearts.Clear();
            }

            for (int i = 0; i < currentHealth; i++)
            {
                var heart = Instantiate(m_heartPrefab, transform);
                m_hearts.Add(heart);
            }
        }
        
    }
}

