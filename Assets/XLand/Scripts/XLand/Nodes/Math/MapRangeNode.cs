using UnityEngine;

namespace XLand.Nodes
{
    [MarkAsNode(DisplayName = "Math/Map Range")]
    public class MapRangeNode : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Input, DisplayName = "in a")]
        public float inA;

        [MarkAsNodeField(Type = NodeFieldType.Input, DisplayName = "in b")]
        public float inB;

        [MarkAsNodeField(Type = NodeFieldType.Input, DisplayName = "out a")]
        public float outA;

        [MarkAsNodeField(Type = NodeFieldType.Input, DisplayName = "out b")]
        public float outB;

        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float input;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float output;

        public override void Evaluate()
        {
            if (inA == inB)
            {
                output = outA;
            }
            else
            {
                var t = ((double) input - (double) inA) / ((double) inB - (double) inA);
                output = Mathf.LerpUnclamped(outA, outB, (float) t);
            }
        }
    }
}