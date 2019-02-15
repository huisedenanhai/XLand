namespace XLand.Nodes
{
    [MarkAsNode(DisplayName = "Input/Value")]
    public class ValueInputNode : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float value;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float output;

        public override void Evaluate()
        {
            output = value;
        }
    }
}