namespace XLand.Nodes.ARKit
{
    [MarkAsNode(DisplayName = "Input/ARKit/Face Rotation")]
    public class FaceRotation : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float x;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float y;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float z;

        public override void Evaluate()
        {
            var faceEuler = ARKitFaceAnchor.FaceEuler;
            x = faceEuler.x;
            y = faceEuler.y;
            z = faceEuler.z;
        }
    }
}