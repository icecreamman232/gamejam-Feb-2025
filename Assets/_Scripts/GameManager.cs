using System;
using System.Collections;
using SGGames.Scripts.Core;
using SGGames.Scripts.Events;
using SGGames.Scripts.UI;
using UnityEngine;

namespace SGGames.Scripts.Managers
{
    public enum GameState
    {
        ADVENTURE,
        COMBAT
    }
    public class GameManager : MonoBehaviour, IGameService
    {
        [SerializeField] private GameState m_gameState = GameState.ADVENTURE;
        [SerializeField] private GameEvents m_gameEvents;
        [SerializeField] private BoolEvent m_playerFrozenEvent;
        [SerializeField] private CombatUIController m_combatUIController;
        
        public GameState GameState => m_gameState;

        private bool m_isCoroutineRunning;
        private WaitForSeconds m_waitBeforeStartCombat;
        private readonly float m_delayBeforeStartCombat = 1.5f;

        private void Start()
        {
            ServiceLocator.RegisterService<GameManager>(this);
            m_waitBeforeStartCombat = new WaitForSeconds(m_delayBeforeStartCombat);
            m_gameEvents.AddListener(OnReceiveGameEvents);
        }

        private void OnDestroy()
        {
            ServiceLocator.UnregisterService<GameManager>();
            m_gameEvents.AddListener(OnReceiveGameEvents);
        }

        public void SetToGameState(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.ADVENTURE:
                    OnAdventureState();
                    break;
                case GameState.COMBAT:
                    OnCombatState();
                    break;
            }
        }

        private void OnAdventureState()
        {
            if (m_isCoroutineRunning) return;
        }

        private void OnCombatState()
        {
            if (m_isCoroutineRunning) return;
            StartCoroutine(OnStartCombatState());
        }

        private IEnumerator OnStartCombatState()
        {
            //Freeze player
            m_playerFrozenEvent.Raise(true);
            yield return m_waitBeforeStartCombat;
            m_combatUIController.OpenCanvas();
        }
        
        private void OnReceiveGameEvents(GameEventsType gameEvents)
        {
            if (gameEvents == GameEventsType.START_COMBAT)
            {
                SetToGameState(GameState.COMBAT);
            }
            else if (gameEvents == GameEventsType.START_ADVENTURE)
            {
                SetToGameState(GameState.ADVENTURE);
            }
        }
    }
}
