using UnityEngine;

namespace XLand.Nodes
{
    [MarkAsNode(DisplayName = "Math/Min")]
    public class MinNode : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float a;

        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float b;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float output;

        public override void Evaluate()
        {
            output = Mathf.Min(a, b);
        }
    }
}