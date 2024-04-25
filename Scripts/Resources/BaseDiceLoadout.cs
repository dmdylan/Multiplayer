using Godot;

[GlobalClass]
public partial class BaseDiceLoadout : Resource
{
	[Export] public DieInfo[] DiceInfo { get; private set; }
}