using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Live2D.Cubism.Core;
using Live2D.Cubism.Framework.Json;
using UnityEngine;

namespace XLand
{
    public class ModelManager
    {
        private static ModelManager m_Instance = null;

        public static ModelManager Instance
        {
            get { return m_Instance ?? (m_Instance = new ModelManager()); }
        }

        private readonly Dictionary<string, CubismModel> m_LoadedModels = new Dictionary<string, CubismModel>();

        private CubismModel m_CurrentActiveModel;

        public CubismModel CurrentActiveModel
        {
            get { return m_CurrentActiveModel; }
        }

        public List<string> LoadedModelPaths
        {
            get { return m_LoadedModels.Keys.ToList(); }
        }

        private string m_CurrentActiveModelPath;

        public delegate void CurrentActiveModelChangedDelegate(string currentActiveModelPath);

        public event CurrentActiveModelChangedDelegate CurrentActiveModelChangedEvent;

        public string CurrentActiveModelPath
        {
            set
            {
                if (value == m_CurrentActiveModelPath) return;
                CubismModel m;
                if (value != null && m_LoadedModels.TryGetValue(value, out m))
                {
                    m_CurrentActiveModelPath = value;
                    m_CurrentActiveModel = m;
                }
                else
                {
                    m_CurrentActiveModelPath = null;
                    m_CurrentActiveModel = null;
                }

                OnCurrentActiveModelChanged(m_CurrentActiveModelPath);
            }
            get { return m_CurrentActiveModelPath; }
        }

        private Transform m_ModelParent;
        public const string modelParentName = "XLandModelParent";

        public Transform ModelParent
        {
            get
            {
                if (m_ModelParent) return m_ModelParent;
                m_ModelParent = new GameObject(modelParentName).transform;
                return m_ModelParent;
            }
        }

        private void EvaluateModelDepth()
        {
            var z = 0.0f;
            foreach (Transform model in ModelParent.transform)
            {
                var pos = model.position;
                pos.z = z;
                model.position = pos;
                z += 1.0f;
            }
        }

        public void LoadModel(string absolutePath)
        {
            if (!m_LoadedModels.ContainsKey(absolutePath))
            {
                // Load model from path
                var model3Json = CubismModel3Json.LoadAtPath(absolutePath, BuiltinLoadAssetAtPath);
                if (model3Json == null) return; // Fail to load model3json
                var model = model3Json.ToModel();
                if (model == null) return; // Fail to load model
                // init transform 
                var mt = model.transform;
                mt.position = Vector3.zero;
                mt.rotation = Quaternion.identity;
                mt.localScale = Vector3.one;
                mt.SetParent(ModelParent, false);
                // make it an XLander
                var xLander = model.gameObject.AddComponent<XLander>();
                xLander.Init(absolutePath);
                // add to loaded model dict
                m_LoadedModels.Add(absolutePath, model);
                EvaluateModelDepth();
            }

            CurrentActiveModelPath = absolutePath; // Make the newly loaded model active
        }

        public void UnloadModel(string absolutePath)
        {
            CubismModel m;
            if (m_LoadedModels.TryGetValue(absolutePath, out m))
            {
                m_LoadedModels.Remove(absolutePath);
                GameObject.Destroy(m.gameObject);
                EvaluateModelDepth();
            }

            if (absolutePath == CurrentActiveModelPath)
            {
                CurrentActiveModelPath = null;
            }
        }

        ///  <summary>
        ///  load asset.
        ///  </summary>
        ///  <param name="assetType"> Asset type. </param>
        ///  <param name="absolutePath"> Path to asset. </param>
        ///  <returns> The asset on success;  <see langword="null">  otherwise.</see> </returns>
        private static object BuiltinLoadAssetAtPath(Type assetType, string absolutePath)
        {
            if (assetType == typeof(byte[]))
            {
                return File.ReadAllBytes(absolutePath);
            }

            if (assetType == typeof(string))
            {
                return File.ReadAllText(absolutePath);
            }

            if (assetType == typeof(Texture2D))
            {
                var texture = new Texture2D(1, 1);
                texture.LoadImage(File.ReadAllBytes(absolutePath));
                return texture;
            }

            throw new NotSupportedException();
        }

        private void OnCurrentActiveModelChanged(string currentActiveModelPath)
        {
            var handler = CurrentActiveModelChangedEvent;
            if (handler != null) handler(currentActiveModelPath);
        }
    }
}