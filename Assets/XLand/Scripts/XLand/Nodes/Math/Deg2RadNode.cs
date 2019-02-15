using UnityEngine;

namespace XLand.Nodes
{
    [MarkAsNode(DisplayName = "Math/Deg2Rad")]
    public class Deg2RadNode : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float deg;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float rad;

        public override void Evaluate()
        {
            rad = deg * Mathf.Deg2Rad;
        }
    }
}