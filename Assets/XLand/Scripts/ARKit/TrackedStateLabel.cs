using UnityEngine;
using UnityEngine.UI;

namespace XLand
{
    [RequireComponent(typeof(Text))]
    public class TrackedStateLabel : MonoBehaviour
    {
        private Text m_Text;

        private void Awake()
        {
            m_Text = GetComponent<Text>();
        }

        private void Update()
        {
            m_Text.text = ARKitFaceAnchor.IsTracked ? "Tracking Face" : "Face Not Found";
        }
    }
}