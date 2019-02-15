namespace XLand.Nodes.ARKit
{
    [MarkAsNode(DisplayName = "Input/ARKit/Look At Point")]
    public class LookAtPoint : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float x;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float y;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float z;

        public override void Evaluate()
        {
            var point = ARKitFaceAnchor.LookAtPoint;
            x = point.x;
            y = point.y;
            z = point.z;
        }
    }
}