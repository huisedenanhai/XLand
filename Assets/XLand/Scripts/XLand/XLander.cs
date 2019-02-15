using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Live2D.Cubism.Core;
using Newtonsoft.Json;
using UnityEngine;
using XLand.Nodes;

namespace XLand
{
    [RequireComponent(typeof(CubismModel))]
    public class XLander : MonoBehaviour
    {
        public const int VERSION = 0;
        private string m_XLanderAbsolutePath;
        private readonly Dictionary<string, DataFlowGraph> m_DataFlowGraphs = new Dictionary<string, DataFlowGraph>();

        public void Init(string modelAbsolutePath)
        {
            var directory = Path.GetDirectoryName(modelAbsolutePath);
            var modelName = Path.GetFileName(modelAbsolutePath).StripAllExtension();
            m_XLanderAbsolutePath = Path.Combine(directory, modelName + ".xlander.json");
        }

        private void LoadData(XLanderData data)
        {
            m_DataFlowGraphs.Clear();
            foreach (var pair in data.graphs)
            {
                var graph = new DataFlowGraph();
                graph.LoadData(pair.Value);
                m_DataFlowGraphs.Add(pair.Key, graph);
            }
        }

        private void LoadData()
        {
            try
            {
                var content = File.ReadAllText(m_XLanderAbsolutePath, Encoding.UTF8);
                var data = JsonConvert.DeserializeObject<XLanderData>(content);
                LoadData(data);
            }
            catch (Exception e)
            {
                // ignored
            }
        }

        private void Start()
        {
            LoadData();
        }

        private XLanderData GetData()
        {
            var graphData = new Dictionary<string, DataFlowGraphData>();
            foreach (var p in m_DataFlowGraphs)
            {
                graphData.Add(p.Key, p.Value.GetData());
            }

            return new XLanderData
            {
                version = VERSION,
                graphs = graphData
            };
        }

        public void Save()
        {
            using (var file = new StreamWriter(m_XLanderAbsolutePath, false, Encoding.UTF8))
            {
                file.Write(JsonConvert.SerializeObject(GetData(), Formatting.Indented));
            }
        }

        public DataFlowGraph GetGraph(string paramName)
        {
            DataFlowGraph graph;
            if (!m_DataFlowGraphs.TryGetValue(paramName, out graph))
            {
                graph = new DataFlowGraph();
                m_DataFlowGraphs.Add(paramName, graph);
            }

            // Ensure there is a param output node
            if (graph.nodes.All(n => n.GetType() != typeof(ParamHandlerNode)))
            {
                graph.nodes.Add(new ParamHandlerNode {paramName = paramName});
            }

            return graph;
        }

        private void LateUpdate()
        {
            foreach (var g in m_DataFlowGraphs.Values)
            {
                g.Evaluate();
            }
        }

        private void OnDestroy()
        {
            Save();
        }

        private Dictionary<string, IParamHandler> m_ParamHandlers;

        private void AddParamHandler(IParamHandler handler)
        {
            m_ParamHandlers.Add(handler.Name, handler);
        }

        private void CollectParamHandlers()
        {
            m_ParamHandlers = new Dictionary<string, IParamHandler>();
            AddParamHandler(new PositionXHandler(transform));
            AddParamHandler(new PositionYHandler(transform));
            AddParamHandler(new ScaleXHandler(transform));
            AddParamHandler(new ScaleYHandler(transform));
            foreach (var parameter in GetComponent<CubismModel>().Parameters)
            {
                AddParamHandler(new CubismParamHandler(parameter));
            }
        }

        public Dictionary<string, IParamHandler> ParamHandlers
        {
            get
            {
                if (m_ParamHandlers == null)
                {
                    CollectParamHandlers();
                }

                return m_ParamHandlers;
            }
        }
    }
}