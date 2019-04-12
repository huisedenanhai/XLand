using UnityEngine;

namespace XLand.Nodes.ARKit
{
    public enum FaceTransformAnchorType
    {
        Dynamic,
        None
    }

    [MarkAsNode(DisplayName = "Input/ARKit/Face Transform")]
    public class FaceTransform : PivotNodeBase
    {
        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public Matrix4x4 matrix;

        [MarkAsNodeField(DisplayName = "pivot type", Type = NodeFieldType.Attribute)]
        public FaceTransformAnchorType pivotType;

        private Matrix4x4 inversePivot = Matrix4x4.identity;

        public override void Evaluate()
        {
            matrix = ARKitFaceAnchor.FaceTransform;
            if (pivotType == FaceTransformAnchorType.None) return;
            matrix *= inversePivot;
        }

        protected override void ResetPivot()
        {
            if (pivotType == FaceTransformAnchorType.None) return;
            inversePivot = ARKitFaceAnchor.FaceInverseTransform;
        }
    }
}