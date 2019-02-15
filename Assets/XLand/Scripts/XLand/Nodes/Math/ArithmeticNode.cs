using System;

namespace XLand.Nodes
{
    public enum ArithmeticType
    {
        Add,
        Sub,
        Mul,
        Div
    }

    [MarkAsNode(DisplayName = "Math/Arithmetic")]
    public class ArithmeticNode : Node
    {
        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float a;

        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float b;

        [MarkAsNodeField(Type = NodeFieldType.Attribute)]
        public ArithmeticType op = ArithmeticType.Add;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float c;

        public override void Evaluate()
        {
            switch (op)
            {
                case ArithmeticType.Add:
                    c = a + b;
                    break;
                case ArithmeticType.Sub:
                    c = a - b;
                    break;
                case ArithmeticType.Mul:
                    c = a * b;
                    break;
                case ArithmeticType.Div:
                    if (b == 0) break;
                    c = a / b;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}