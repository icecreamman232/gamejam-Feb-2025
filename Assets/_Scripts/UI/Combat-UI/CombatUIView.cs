using UnityEngine;

namespace SGGames.Scripts.UI
{
    public class CombatUIView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup m_canvasGroup;
        
        public void ShowView()
        {
            m_canvasGroup.alpha = 1;
            m_canvasGroup.interactable = true;
            m_canvasGroup.blocksRaycasts = true;
        }

        public void HideView()
        {
            m_canvasGroup.alpha = 0;
            m_canvasGroup.interactable = false;
            m_canvasGroup.blocksRaycasts = false;
        }
        
    }
}
