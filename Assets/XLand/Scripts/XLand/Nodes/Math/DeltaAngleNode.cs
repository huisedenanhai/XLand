using UnityEngine;

namespace XLand.Nodes
{
    [MarkAsNode(DisplayName = "Math/Delta Angle")]
    public class DeltaAngleNode : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float current;

        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float target;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float output;

        public override void Evaluate()
        {
            output = Mathf.DeltaAngle(current, target);
        }
    }
}