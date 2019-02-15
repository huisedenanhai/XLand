namespace XLand.Nodes
{
    [MarkAsNode(DisplayName = "Debug/Node Local Position")]
    public class NodeLocalPositionDisplayNode : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Attribute)]
        public float x;

        [MarkAsNodeField(Type = NodeFieldType.Attribute)]
        public float y;

        public override void Evaluate()
        {
            x = position.x;
            y = position.y;
        }
    }
}