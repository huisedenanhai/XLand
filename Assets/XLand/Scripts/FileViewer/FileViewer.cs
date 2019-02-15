using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace XLand
{
    public class FileViewer : MonoBehaviour
    {
        private string m_CurrentDirectory;
        [SerializeField] private PrefabPool m_DirectoryEntryPool;
        [SerializeField] private PrefabPool m_FileEntryPool;
        [SerializeField] private GameObject m_ScrollViewContent;
        [SerializeField] private Text m_CurrentDirectoryLabel;

        public string CurrentDirectory
        {
            get { return m_CurrentDirectory ?? Application.persistentDataPath; }
            set
            {
                var v = value ?? Application.persistentDataPath;

                if (m_CurrentDirectory == v) return;
                m_CurrentDirectory = v;
                m_CurrentDirectoryLabel.text = v;
                m_ViewNeedUpdate = true;
            }
        }

        private void Start()
        {
            CurrentDirectory = Application.persistentDataPath;
        }

        public void BackToParentDirectory()
        {
            if (CurrentDirectory == Application.persistentDataPath) return;
            CurrentDirectory = Path.GetDirectoryName(CurrentDirectory);
        }

        private bool m_ViewNeedUpdate = true;

        private void ClearView()
        {
            var fileEntries = new List<Transform>();
            var directoryEntries = new List<Transform>();
            foreach (Transform child in m_ScrollViewContent.transform)
            {
                if (child.GetComponent<FileEntry>() != null)
                {
                    fileEntries.Add(child);
                    continue;
                }

                if (child.GetComponent<DirectoryEntry>() != null)
                {
                    directoryEntries.Add(child);
                    continue;
                }
            }

            foreach (var child in fileEntries)
            {
                m_FileEntryPool.Put(child.gameObject);
            }

            foreach (var child in directoryEntries)
            {
                m_DirectoryEntryPool.Put(child.gameObject);
            }
        }

        private void UpdateView()
        {
            ClearView();
            foreach (var directoryName in Directory.GetDirectories(CurrentDirectory))
            {
                var go = m_DirectoryEntryPool.Get();
                var entry = go.GetComponent<DirectoryEntry>();
                entry.Init(directoryName);
                go.transform.SetParent(m_ScrollViewContent.transform, false);
            }

            foreach (var fileName in Directory.GetFiles(CurrentDirectory))
            {
                var go = m_FileEntryPool.Get();
                var entry = go.GetComponent<FileEntry>();
                entry.Init(fileName);
                go.transform.SetParent(m_ScrollViewContent.transform, false);
            }
        }

        private void LateUpdate()
        {
            if (m_ViewNeedUpdate)
            {
                UpdateView();
                m_ViewNeedUpdate = false;
            }
        }
    }
}