using UnityEngine;

namespace XLand.Nodes
{
    [MarkAsNode(DisplayName = "Output/Value Inspector")]
    public class ValueInspectorNode : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float value;

        public override void Evaluate()
        {
            // do nothing, just display input value
        }
    }
}