using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace XLand
{
    public class FileEntry : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Text m_NameLabel;
        public bool hideFileViewerAfterLoad = true;
        private string m_FullPath;

        private bool IsModel3Json
        {
            get { return m_FullPath.EndsWith(".model3.json"); }
        }

        public void Init(string fileName)
        {
            m_FullPath = fileName;
            m_NameLabel.text = Path.GetFileName(fileName);
            m_NameLabel.fontStyle = IsModel3Json ? FontStyle.Bold : FontStyle.Normal;
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if (!IsModel3Json) return;
            ModelManager.Instance.LoadModel(m_FullPath);
            if (!hideFileViewerAfterLoad) return;
            var selectActive = GetComponentInParent<SelectActiveChild>();
            if (selectActive == null) return;
            selectActive.CurrentActiveChild = null;
        }
    }
}