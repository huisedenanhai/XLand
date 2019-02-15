namespace XLand.Nodes
{
    [MarkAsNode(DisplayName = "Math/Activation/LeakyReLU")]
    public class LeakyReLU : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float input;

        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float slop;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float output;

        public override void Evaluate()
        {
            output = input > 0 ? input : input * slop;
        }
    }
}