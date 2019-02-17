namespace XLand.Nodes
{
    public enum PivotMode
    {
        Dynamic,
        Static
    }

    [MarkAsNode(HideInInspector = true)]
    public abstract class PivotNodeBase : Node
    {
        public PivotNodeBase()
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

        protected abstract void ResetPivot();
    }
}