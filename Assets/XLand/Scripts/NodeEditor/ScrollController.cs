using UnityEngine;
using UnityEngine.UI;

namespace XLand
{
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollController : MonoBehaviour
    {
        private ScrollRect m_ScrollRect;

        private void Awake()
        {
            m_ScrollRect = GetComponent<ScrollRect>();
        }

        public bool CanScroll
        {
            get { return m_ScrollRect.horizontal || m_ScrollRect.vertical; }
            set { m_ScrollRect.horizontal = m_ScrollRect.vertical = value; }
        }
    }
}