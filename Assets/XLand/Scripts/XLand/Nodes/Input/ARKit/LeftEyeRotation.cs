namespace XLand.Nodes.ARKit
{
    [MarkAsNode(DisplayName = "Input/ARKit/Left Eye Rotation")]
    public class LeftEyeRotation : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float x;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float y;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float z;

        public override void Evaluate()
        {
            var euler = ARKitFaceAnchor.LeftEyeEuler;
            x = euler.x;
            y = euler.y;
            z = euler.z;
        }
    }
}