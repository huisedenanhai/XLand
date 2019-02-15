using UnityEngine;

namespace XLand.Nodes
{
    [MarkAsNode(DisplayName = "Math/Clamp")]
    public class ClampNode : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float value;

        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float min;

        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float max;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float output;

        public override void Evaluate()
        {
            output = Mathf.Clamp(value, min, max);
        }
    }
}