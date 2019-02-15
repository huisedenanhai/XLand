using UnityEngine;

namespace XLand.Nodes
{
    [MarkAsNode(DisplayName = "Math/Sign")]
    public class SignNode : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float value;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float sign;

        public override void Evaluate()
        {
            sign = Mathf.Sign(value);
        }
    }
}