using UnityEngine;
using UnityEngine.XR.iOS;

namespace XLand.Nodes.ARKit
{
    [MarkAsNode(DisplayName = "Input/ARKit/Get Position")]
    public class GetPosition : Node
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
            var pos = UnityARMatrixOps.GetPosition(matrix);
            x = pos.x;
            y = pos.y;
            z = pos.z;
        }
    }
}