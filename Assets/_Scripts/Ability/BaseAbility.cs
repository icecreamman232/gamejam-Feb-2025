
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
        private int m_attackRemaining;
        private int m_blockRemaining;
        private AbilityData m_data;
        
        
        public AbilityData Data => m_data;
        public AbilityState CurrentState => m_currentState;

        public int AttackRemaining => m_attackRemaining;
        public int BlockRemaining => m_blockRemaining;
        
        public BaseAbility(AbilityData data)
        {
            m_data = data;
            m_currentState = AbilityState.READY;
            m_attackRemaining = m_data.AttackCount;
            m_blockRemaining = m_data.BlockCount;
        }

        public void DoAttack()
        {
            if (m_currentState == AbilityState.COOLDOWN) return;
            m_attackRemaining--;
            if (m_attackRemaining <= 0)
            {
                SetState(AbilityState.COOLDOWN);
            }
        }
        
        public void DoDefense()
        {
            if (m_currentState == AbilityState.COOLDOWN) return;
            m_blockRemaining--;
            if (m_blockRemaining <= 0)
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

