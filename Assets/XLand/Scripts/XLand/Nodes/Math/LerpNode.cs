using UnityEngine;

namespace XLand.Nodes
{
    [MarkAsNode(DisplayName = "Math/Lerp")]
    public class LerpNode : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float a;

        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float b;

        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float t;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float output;

        public override void Evaluate()
        {
            output = Mathf.Lerp(a, b, t);
        }
    }
}