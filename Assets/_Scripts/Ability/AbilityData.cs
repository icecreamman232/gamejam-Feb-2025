using UnityEngine;

namespace SGGames.Scripts.Abilities
{
    [CreateAssetMenu(menuName = "SGGames/Ability")]
    public class AbilityData : ScriptableObject
    {
        [SerializeField] private Sprite m_icon;
        [SerializeField] private float m_minDamage;
        [SerializeField] private float m_maxDamage;
        [SerializeField] private int m_attackSpeed; //How many attack before ability goes cooldown
        [SerializeField] private int m_cooldown;
        
        public Sprite Icon => m_icon;
        public float MinDamage => m_minDamage;
        public float MaxDamage => m_maxDamage;
        public int AttackSpeed => m_attackSpeed;
        public int Cooldown => m_cooldown;
    }
}
