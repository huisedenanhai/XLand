using System;
using System.Collections.Generic;
using UnityEngine;

namespace XLand
{
    public class LoadedModelViewerContent : MonoBehaviour
    {
        [SerializeField] private PrefabPool m_ModelSelectorPool;

        private void ClearContent()
        {
            var children = new List<Transform>();
            foreach (Transform child in transform)
            {
                children.Add(child);
            }

            foreach (var child in children)
            {
                m_ModelSelectorPool.Put(child.gameObject);
            }
        }

        private void RefreshContent()
        {
            ClearContent();

            foreach (var path in ModelManager.Instance.LoadedModelPaths)
            {
                var selector = m_ModelSelectorPool.Get().GetComponent<ModelSelector>();
                selector.ModelAbsolutePath = path;
                selector.transform.SetParent(transform, false);
            }
        }

        private void OnEnable()
        {
            SetNeedRefresh();
        }

        private bool m_NeedRefresh;

        public void SetNeedRefresh()
        {
            m_NeedRefresh = true;
        }

        private void LateUpdate()
        {
            if (m_NeedRefresh)
            {
                RefreshContent();
                m_NeedRefresh = false;
            }
        }
    }
}