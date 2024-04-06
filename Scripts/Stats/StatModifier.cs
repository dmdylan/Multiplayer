public enum StatModifierType{ Flat, PercentAdditive, PercentMultiplicitive }

public class StatModifier
{
	public StatModifierType StatModifierType { get; private set; }
	public float Value { get; private set;}

	public StatModifier(StatModifierType statModifierType, float value)
	{
		StatModifierType = statModifierType;
		Value = value;
	}
}