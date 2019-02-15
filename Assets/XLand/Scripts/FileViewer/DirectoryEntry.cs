using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace XLand
{
    public class DirectoryEntry : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Text m_NameLabel;
        private string m_FullPath;

        public void Init(string directoryName)
        {
            m_FullPath = directoryName;
            m_NameLabel.text = Path.GetFileName(directoryName);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GetComponentInParent<FileViewer>().CurrentDirectory = m_FullPath;
        }
    }
}