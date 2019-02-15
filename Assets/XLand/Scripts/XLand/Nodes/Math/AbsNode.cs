using UnityEngine;

namespace XLand.Nodes
{
    [MarkAsNode(DisplayName = "Math/Abs")]
    public class AbsNode : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float value;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float abs;

        public override void Evaluate()
        {
            abs = Mathf.Abs(value);
        }
    }
}