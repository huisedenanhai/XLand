using UnityEngine;

namespace XLand.Nodes
{
    [MarkAsNode(DisplayName = "Math/Repeat")]
    public class RepeatNode : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float t;

        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float length;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float output;

        public override void Evaluate()
        {
            output = Mathf.Repeat(t, length);
        }
    }
}