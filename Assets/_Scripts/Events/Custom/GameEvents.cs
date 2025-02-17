using System;
using UnityEngine;

namespace SGGames.Scripts.Events
{
    public enum GameEventsType
    {
        START_COMBAT,
        START_ADVENTURE,
    }
    
    [CreateAssetMenu(menuName = "SGGames/Event/Game Event")]
    public class GameEvents : ScriptableObject
    {
        protected Action<GameEventsType> m_listeners;
        
        public void AddListener(Action<GameEventsType> addListener)
        {
            m_listeners += addListener;
        }

        public void RemoveListener(Action<GameEventsType> removeListener)
        {
            m_listeners -= removeListener;
        }

        public void Raise(GameEventsType gameEvent)
        {
            m_listeners?.Invoke(gameEvent);
        }
    } 
}