using System;
using UnityEngine;
using UnityEngine.XR.iOS;

namespace XLand.Nodes.ARKit
{
    [MarkAsNode(DisplayName = "Input/ARKit/Get Rotation")]
    public class GetRotation : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float x;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float y;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float z;

        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public Matrix4x4 matrix;

        public override void Evaluate()
        {
            var eular = UnityARMatrixOps.GetRotation(matrix).eulerAngles;
            x = eular.x;
            y = eular.y;
            z = eular.z;
        }
    }
}