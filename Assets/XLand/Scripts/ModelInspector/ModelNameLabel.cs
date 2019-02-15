using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace XLand
{
    [RequireComponent(typeof(Text))]
    public class ModelNameLabel : MonoBehaviour
    {
        private Text m_Text;

        private void Awake()
        {
            ModelManager.Instance.CurrentActiveModelChangedEvent += OnCurrentActiveModelChanged;
            m_Text = GetComponent<Text>();
        }

        private void OnCurrentActiveModelChanged(string currentActiveModelPath)
        {
            if (currentActiveModelPath == null)
            {
                m_Text.text = "No Model Selected";
                return;
            }

            m_Text.text = Path.GetFileName(currentActiveModelPath).StripAllExtension();
        }
    }
}