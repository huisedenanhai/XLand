using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace XLand
{
    public class AddNodeMenuFolderEntry : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Text m_FolderNameLabel;

        private string m_FullPath;

        public string FullPath
        {
            get { return m_FullPath; }
            set
            {
                m_FullPath = value;
                m_FolderNameLabel.text = Path.GetFileName(value ?? "");
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GetComponentInParent<AddNodeMenu>().CurrentDirectory = FullPath;
        }
    }
}