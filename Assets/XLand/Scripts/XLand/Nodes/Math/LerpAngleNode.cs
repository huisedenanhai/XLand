using System;
using UnityEngine;

namespace XLand.Nodes
{
    [MarkAsNode(DisplayName = "Math/Lerp Angle")]
    public class LerpAngleNode : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float a;

        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float b;

        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float t;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float output;

        public override void Evaluate()
        {
            output = Mathf.LerpAngle(a, b, t);
        }
    }
}