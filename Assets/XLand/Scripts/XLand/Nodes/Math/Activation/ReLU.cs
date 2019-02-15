namespace XLand.Nodes
{
    [MarkAsNode(DisplayName = "Math/Activation/ReLU")]
    public class ReLU : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float input;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float output;

        public override void Evaluate()
        {
            output = input > 0 ? input : 0;
        }
    }
}