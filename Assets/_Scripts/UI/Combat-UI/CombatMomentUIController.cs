using UnityEngine;
using UnityEngine.UI;

namespace SGGames.Scripts.UI
{
    public class CombatMomentUIController : MonoBehaviour
    {
        [SerializeField] private Image m_icon;

        public void SetMoment(Sprite icon)
        {
            m_icon.sprite = icon;
        }
    }
}
