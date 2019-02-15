using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace XLand
{
    public class AddNodeMenu : MonoBehaviour
    {
        public AddNodeMenuEntry menuEntryPrefab;
        public AddNodeMenuFolderEntry menuFolderEntryPrefab;
        [SerializeField] private Transform m_Content;
        [SerializeField] private Text m_CurrentDirectoryLabel;


        private AddNodeMenuData m_RootData;

        private AddNodeMenuData RootData
        {
            get
            {
                if (m_RootData == null)
                {
                    BuildRootData();
                }

                return m_RootData;
            }
        }

        private void AddNodeInfoToRoot(NodeInfo nodeInfo, string displayName)
        {
            var nodeName = Path.GetFileName(displayName);
            var directory = Path.GetDirectoryName(displayName);
            var currentData = m_RootData;
            foreach (var folder in directory.GetFolders())
            {
                var newData = currentData[folder];
                if (newData == null)
                {
                    newData = currentData.Add(folder, new AddNodeMenuData());
                    if (newData == null) break;
                }

                currentData = newData;
            }

            currentData.Add(nodeName, new AddNodeMenuData {NodeInfo = nodeInfo});
        }

        private void BuildRootData()
        {
            m_RootData = new AddNodeMenuData();
            foreach (var nodeInfo in Node.NodeInfos)
            {
                var attrs = nodeInfo.type.GetCustomAttributes(typeof(MarkAsNode), false);
                var displayName = nodeInfo.type.GetDisplayName();
                if (attrs.Length != 0)
                {
                    var attr = attrs[0] as MarkAsNode;
                    if (attr.HideInInspector) continue;
                    if (attr.DisplayName != null) displayName = attr.DisplayName;
                }

                AddNodeInfoToRoot(nodeInfo, displayName);
            }
        }

        private string m_CurrentDirectory;

        public string CurrentDirectory
        {
            get { return m_CurrentDirectory; }
            set
            {
                if (value == null) value = "";
                var sb = new StringBuilder();
                var cd = RootData;
                foreach (var folder in value.GetFolders())
                {
                    var data = cd[folder];
                    if (data == null || data.IsNodeEntry) break;
                    cd = data;
                    sb.Append("/");
                    sb.Append(folder);
                }

                CurrentData = cd;
                var dir = sb.ToString();
                if (dir == "") dir = "/";
                m_CurrentDirectory = dir;
                m_CurrentDirectoryLabel.text = m_CurrentDirectory;
            }
        }

        private AddNodeMenuData m_CurrentData;

        private AddNodeMenuData CurrentData
        {
            get { return m_CurrentData ?? RootData; }

            set
            {
                if (m_CurrentData == value) return;
                m_CurrentData = value;
                NeedRefresh();
            }
        }

        public void BackToUpperFolder()
        {
            CurrentDirectory = Path.GetDirectoryName(CurrentDirectory);
        }

        private bool m_NeedRefresh;

        private void NeedRefresh()
        {
            m_NeedRefresh = true;
        }

        private void Start()
        {
            CurrentDirectory = "/";
        }

        private void RefreshView()
        {
            foreach (Transform child in m_Content)
            {
                Destroy(child.gameObject);
            }

            foreach (var p in CurrentData.Data.Where(p => !p.Value.IsNodeEntry))
            {
                var entry = Instantiate(menuFolderEntryPrefab, m_Content, false);
                entry.FullPath = CurrentDirectory + "/" + p.Key;
            }

            foreach (var p in CurrentData.Data.Where(p => p.Value.IsNodeEntry))
            {
                var entry = Instantiate(menuEntryPrefab, m_Content, false);
                entry.NodeInfo = p.Value.NodeInfo;
            }
        }

        private void LateUpdate()
        {
            if (m_NeedRefresh)
            {
                RefreshView();
                m_NeedRefresh = false;
            }
        }
    }
}