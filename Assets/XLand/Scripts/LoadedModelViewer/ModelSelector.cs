using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace XLand
{
    public class ModelSelector : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Text m_NameLabel;
        [SerializeField] private Button m_UnloadButton;
        private string m_ModelAbsolutePath;

        private void Awake()
        {
            m_UnloadButton.onClick.AddListener(UnloadModel);
        }

        public string ModelAbsolutePath
        {
            set
            {
                m_ModelAbsolutePath = value;
                m_NameLabel.text = Path.GetFileName(value).StripAllExtension();
            }
            get { return m_ModelAbsolutePath; }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ModelManager.Instance.CurrentActiveModelPath = m_ModelAbsolutePath;
        }

        private void UnloadModel()
        {
            ModelManager.Instance.UnloadModel(m_ModelAbsolutePath);
            GetComponentInParent<LoadedModelViewerContent>().SetNeedRefresh();
        }
    }
}