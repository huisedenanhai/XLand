using UnityEngine;

namespace XLand.Nodes
{
    [MarkAsNode(DisplayName = "Math/Rad2Deg")]
    public class Rad2DegNode : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float rad;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float deg;

        public override void Evaluate()
        {
            deg = rad * Mathf.Rad2Deg;
        }
    }
}