using UnityEngine;

namespace XLand.Nodes
{
    [MarkAsNode(DisplayName = "Math/Perlin Noise")]
    public class PerlinNoiseNode : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float x;

        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float y;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float output;

        public override void Evaluate()
        {
            output = Mathf.PerlinNoise(x, y);
        }
    }
}