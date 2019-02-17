using System;

namespace XLand.Nodes
{


    [MarkAsNode(DisplayName = "Control/Float Pivot")]
    public class FloatPivotNode : PivotNodeBase
    {
        [MarkAsNodeField(Type = NodeFieldType.Input)]
        public float input;

        [MarkAsNodeField(Type = NodeFieldType.Attribute)]
        public float pivot;

        [MarkAsNodeField(Type = NodeFieldType.Attribute)]
        public PivotMode mode = PivotMode.Dynamic;

        [MarkAsNodeField(Type = NodeFieldType.Output)]
        public float displacement;

        [MarkAsNodeField(Type = NodeFieldType.Output, DisplayName = "pivot")]
        public float pivotOutput;

        protected override void ResetPivot()
        {
            switch (mode)
            {
                case PivotMode.Dynamic:
                    pivot = input;
                    break;
                case PivotMode.Static:
                    // do nothing
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void Evaluate()
        {
            displacement = input - pivot;
            pivotOutput = pivot;
        }
    }
}