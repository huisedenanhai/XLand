using System.Collections.Generic;
using Live2D.Cubism.Core;
using UnityEngine;

namespace XLand
{
    public class ParamsInspector : MonoBehaviour
    {
        [SerializeField] private PrefabPool m_ParamControlEntryPool;
        [SerializeField] private NodeEditor m_NodeEditor;
        [SerializeField] private SelectActiveChild m_DetailedPanel;

        private bool m_NeedRefresh = true;
        private CubismModel m_CurrentActiveModel;

        private CubismModel CurrentActiveModel
        {
            set
            {
                if (value == m_CurrentActiveModel) return;
                m_CurrentActiveModel = value;
                m_NeedRefresh = true;
            }
            get { return m_CurrentActiveModel; }
        }

        private void Awake()
        {
            ModelManager.Instance.CurrentActiveModelChangedEvent += OnCurrentActiveModelChanged;
        }

        private void OnCurrentActiveModelChanged(string currentActiveModelPath)
        {
            CurrentActiveModel = ModelManager.Instance.CurrentActiveModel;
        }

        public void ResetValues()
        {
            foreach (var entry in GetComponentsInChildren<ParamControlEntry>())
            {
                entry.ResetValue();
            }
        }

        private void ClearContent()
        {
            var children = new List<Transform>();
            foreach (Transform child in transform)
            {
                children.Add(child);
            }

            foreach (var child in children)
            {
                m_ParamControlEntryPool.Put(child.gameObject);
            }
        }

        private void AddHandler(IParamHandler handler)
        {
            var controlEntry = m_ParamControlEntryPool.Get().GetComponent<ParamControlEntry>();
            controlEntry.Handler = handler;
            controlEntry.OnNodeButtonClicked = () =>
            {
                m_DetailedPanel.CurrentActiveChild = m_NodeEditor.transform;
                m_NodeEditor.CurrentParamHandler = handler;
            };
            controlEntry.transform.SetParent(transform, false);
        }

        private void RefreshView()
        {
            ClearContent();
            if (CurrentActiveModel == null) return;
            AddHandler(new PositionXHandler(CurrentActiveModel.transform));
            AddHandler(new PositionYHandler(CurrentActiveModel.transform));
            AddHandler(new ScaleXHandler(CurrentActiveModel.transform));
            AddHandler(new ScaleYHandler(CurrentActiveModel.transform));
            foreach (var parameter in CurrentActiveModel.Parameters)
            {
                AddHandler(new CubismParamHandler(parameter));
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