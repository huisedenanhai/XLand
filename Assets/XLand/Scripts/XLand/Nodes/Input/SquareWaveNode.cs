using UnityEngine;

namespace XLand.Nodes
{
    [MarkAsNode(DisplayName = "Input/Square Wave")]
    public class SquareWaveNode : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float amplitude = 1.0f;

        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float period = 1.0f;

        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float phase = 0.0f;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float output;


        public override void Evaluate()
        {
            if (period == 0) return;
            var v = Mathf.Repeat(Time.time / period + phase, 1.0f);
            output = v > 0.5f ? amplitude : 0;
        }
    }
}