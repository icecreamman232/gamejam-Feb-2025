
using System;
using SGGames.Scripts.Abilities;


namespace SGGames.SGGames.Scripts.Abilities
{
    public enum AbilityState
    {
        READY,
        COOLDOWN,
    }
    
    [Serializable]
    public class BaseAbility
    {
        private AbilityState m_currentState;
        private int m_attackTimeRemaining;
        private AbilityData m_data;
        
        
        public AbilityData Data => m_data;
        public AbilityState CurrentState => m_currentState;

        public int AttackRemaining => m_attackTimeRemaining;
        
        public BaseAbility(AbilityData data)
        {
            m_data = data;
            m_currentState = AbilityState.READY;
            m_attackTimeRemaining = m_data.AttackCount;
        }

        public void DoAttack()
        {
            if (m_currentState == AbilityState.COOLDOWN) return;
            m_attackTimeRemaining--;
            if (m_attackTimeRemaining <= 0)
            {
                SetState(AbilityState.COOLDOWN);
            }
        }

        public void SetState(AbilityState newState)
        {
            m_currentState = newState;
        }
    }
}

