using UnityEngine;

namespace XLand.Nodes
{
    [MarkAsNode(DisplayName = "Input/Triangle Wave")]
    public class TriangleWaveNode : Node
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
            output = amplitude * Mathf.PingPong(Time.time / period + phase, 0.5f) * 2.0f;
        }
    }
}