using UnityEngine;

namespace XLand.Nodes
{
    [MarkAsNode(DisplayName = "Math/Cos")]
    public class CosNode: Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float input;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float output;

        public override void Evaluate()
        {
            output = Mathf.Cos(input);
        }
    }
}