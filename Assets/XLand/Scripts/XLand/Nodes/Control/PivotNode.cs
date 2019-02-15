using System;

namespace XLand.Nodes
{
    public enum PivotMode
    {
        Dynamic,
        Static
    }

    [MarkAsNode(DisplayName = "Control/Pivot")]
    public class PivotNode : Node
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

        public PivotNode()
        {
            ResetPivotEvent += ResetPivot;
        }

        public override void OnDeleted()
        {
            ResetPivotEvent -= ResetPivot;
        }

        public delegate void ResetPivotDelegate();

        public static event ResetPivotDelegate ResetPivotEvent;

        public static void TriggerResetPivot()
        {
            if (ResetPivotEvent != null) ResetPivotEvent.Invoke();
        }

        public void ResetPivot()
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