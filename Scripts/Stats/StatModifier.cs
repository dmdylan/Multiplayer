public enum StatModifierType{ Flat = 100, PercentAdditive = 200, PercentMultiplicitive = 300 }

public class StatModifier
{
	public float Value { get; private set;}
	public StatModifierType StatModifierType { get; private set; }
	public int Order { get; private set; }
	public object Source { get; private set; }

	public StatModifier(float value, StatModifierType statModifierType, int order, object source)
	{
		Value = value;
		StatModifierType = statModifierType;
		Order = order;
		Source = source;
	}
	
	public StatModifier(float value, StatModifierType statModifierType) : this(value, statModifierType, (int) statModifierType, null) {}
	public StatModifier(float value, StatModifierType statModifierType, int order) : this(value, statModifierType, order, null) {}
	public StatModifier(float value, StatModifierType statModifierType, object source) : this(value, statModifierType, (int) statModifierType, source) {}
}