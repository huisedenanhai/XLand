using UnityEngine;

namespace XLand.Nodes
{
    [MarkAsNode(DisplayName = "Output/Param Handler", HideInInspector = true, CanBeDeleted = false)]
    public class ParamHandlerNode : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float value;

        [MarkAsNodeField(Type = NodeFieldType.Attribute, DisplayName = "param")]
        public string paramName;

        private IParamHandler m_Handler;

        public override void OnCreated(XLander xLander)
        {
            IParamHandler handler;
            if (xLander.ParamHandlers.TryGetValue(paramName, out handler))
            {
                m_Handler = handler;
            }
        }

        public override void Evaluate()
        {
            if (m_Handler == null) return;
            if (IsConnectedInputField("value"))
            {
                // this node driven by the graph
                m_Handler.Value = Mathf.Clamp(value, m_Handler.MinimumValue, m_Handler.MaximumValue);
            }
            else
            {
                // just display the value of param
                value = m_Handler.Value;
            }
        }
    }
}