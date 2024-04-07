using Godot;

[GlobalClass]
public partial class BaseStatPair : Resource
{
	[Export] public StatType StatType { get; private set; }
	[Export] public float Value { get; private set; }
}

public enum StatType
{
	Health,
	Speed
}