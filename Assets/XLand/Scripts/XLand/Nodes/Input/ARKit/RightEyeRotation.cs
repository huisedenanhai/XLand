namespace XLand.Nodes.ARKit
{
    [MarkAsNode(DisplayName = "Input/ARKit/Right Eye Rotation")]
    public class RightEyeRotation : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float x;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float y;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float z;

        public override void Evaluate()
        {
            var euler = ARKitFaceAnchor.RightEyeEuler;
            x = euler.x;
            y = euler.y;
            z = euler.z;
        }
    }
}