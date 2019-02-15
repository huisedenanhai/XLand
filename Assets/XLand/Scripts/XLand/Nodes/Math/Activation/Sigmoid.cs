using UnityEngine;

namespace XLand.Nodes
{
    [MarkAsNode(DisplayName = "Math/Activation/Sigmoid")]
    public class Sigmoid : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float input;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float output;

        public override void Evaluate()
        {
            output = 1.0f / (1.0f + Mathf.Exp(-input));
        }
    }
}