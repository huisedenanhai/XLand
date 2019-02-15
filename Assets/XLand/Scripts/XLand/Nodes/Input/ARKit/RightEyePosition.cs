namespace XLand.Nodes.ARKit
{
    [MarkAsNode(DisplayName = "Input/ARKit/Right Eye Position")]
    public class RightEyePosition : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float x;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float y;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float z;

        public override void Evaluate()
        {
            var pos = ARKitFaceAnchor.RightEyePosition;
            x = pos.x;
            y = pos.y;
            z = pos.z;
        }
    }
}