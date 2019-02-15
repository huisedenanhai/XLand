using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace XLand
{
    public class ChangeColorOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private Graphic m_Graphic;

        private Graphic Graphic
        {
            get
            {
                if (m_Graphic == null)
                {
                    m_Graphic = GetComponent<Graphic>();
                }

                return m_Graphic;
            }
        }

        public Color hoverColor = Color.red;
        public Color normalColor = Color.white;

        public void OnPointerEnter(PointerEventData eventData)
        {
            Graphic.color = hoverColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Graphic.color = normalColor;
        }
    }
}