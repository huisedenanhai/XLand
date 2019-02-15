namespace XLand.Nodes.ARKit
{
    [MarkAsNode(DisplayName = "Input/ARKit/Left Eye Position")]
    public class LeftEyePosition : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float x;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float y;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float z;

        public override void Evaluate()
        {
            var pos = ARKitFaceAnchor.LeftEyePosition;
            x = pos.x;
            y = pos.y;
            z = pos.z;
        }
    }
}