namespace XLand
{
    public interface IParamHandler
    {
        string Name { get; }
        float Value { get; set; }
        float DefaultValue { get; }
        float MinimumValue { get; }
        float MaximumValue { get; }
    }
}