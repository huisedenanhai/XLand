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

        private IParamHandler Handler
        {
            get
            {
                if (m_Handler == null)
                {
                    m_Handler = ModelManager.Instance.CurrentActiveModel.GetComponent<XLander>()
                        .ParamHandlers[paramName];
                }

                return m_Handler;
            }
        }

        public override void Evaluate()
        {
            if (IsConnectedInputField("value"))
            {
                // this node driven by the graph
                Handler.Value = Mathf.Clamp(value, Handler.MinimumValue, Handler.MaximumValue);
            }
            else
            {
                // just display the value of param
                value = Handler.Value;
            }
        }
    }
}