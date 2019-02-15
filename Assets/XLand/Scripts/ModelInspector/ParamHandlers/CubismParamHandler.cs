using Live2D.Cubism.Core;

namespace XLand
{
    public class CubismParamHandler : IParamHandler
    {
        private readonly CubismParameter m_Parameter;

        public CubismParamHandler(CubismParameter parameter)
        {
            m_Parameter = parameter;
        }

        public string Name
        {
            get { return m_Parameter.Id; }
        }

        public float Value
        {
            get { return m_Parameter.Value; }
            set { m_Parameter.Value = value; }
        }

        public float DefaultValue
        {
            get { return m_Parameter.DefaultValue; }
        }

        public float MinimumValue
        {
            get { return m_Parameter.MinimumValue; }
        }

        public float MaximumValue
        {
            get { return m_Parameter.MaximumValue; }
        }
    }
}