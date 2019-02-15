using UnityEngine;
using UnityEngine.UI;

namespace XLand
{
    public class ChangeColorWhenTracked : MonoBehaviour
    {
        public Color normalColor = Color.white;
        public Color activeColor = Color.red;
        private Graphic m_Graphic;

        private void Awake()
        {
            m_Graphic = GetComponent<Graphic>();
        }

        private void Update()
        {
            m_Graphic.color = ARKitFaceAnchor.IsTracked ? activeColor : normalColor;
        }
    }
}