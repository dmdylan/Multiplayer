using Godot;

[GlobalClass]
public partial class BaseStatPair : Resource
{
	//change from string name to enum
	[Export] public string Name { get; private set; }
	[Export] public float Value { get; private set; }
}