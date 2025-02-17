using UnityEngine;

namespace SGGames.Scripts.Abilities
{
    public enum AbilityType
    {
        Offense,
        Defense,
    }
    
    [CreateAssetMenu(menuName = "SGGames/Ability")]
    public class AbilityData : ScriptableObject
    {
        [SerializeField] private AbilityType m_abilityType;
        [SerializeField] private Sprite m_icon;
        [SerializeField] private int m_cooldown;
        [Header("Offense")]
        [SerializeField] private float m_minDamage;
        [SerializeField] private float m_maxDamage;
        [SerializeField] private int m_attackCount; //How many attack before ability goes cooldown
        [Header("Defense")]
        [SerializeField] private float m_minDefense;
        [SerializeField] private float m_maxDefense;
        [SerializeField] private int m_blockCount;
        
        public AbilityType AbilityType => m_abilityType;
        public int Cooldown => m_cooldown;
        public Sprite Icon => m_icon;
        
        //Offense
        public float MinDamage => m_minDamage;
        public float MaxDamage => m_maxDamage;
        public int AttackCount => m_attackCount;
        
        //Defense
        public float MinDefense => m_minDefense;
        public float MaxDefense => m_maxDefense;
        public int BlockCount => m_blockCount;
    }
}
