using System;
using UnityEngine;

namespace XLand.Nodes
{
    [MarkAsNode(DisplayName = "Input/Sin Wave")]
    public class SinWaveNode : Node
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
            output = amplitude * Mathf.Sin(2 * Mathf.PI * Time.time / period + phase);
        }
    }
}