using UnityEngine;

namespace XLand.Nodes
{
    [MarkAsNode(DisplayName = "Input/Time")]
    public class TimeNode : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float output;

        public override void Evaluate()
        {
            output = Time.time;
        }
    }
}