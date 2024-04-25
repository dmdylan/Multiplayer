using Godot;

public enum DieType { D4, D6, D8}

[GlobalClass]
public partial class DieInfo : Resource
{
	[Export] public DieFace[] DieFaces { get; private set; }
	[Export] public DieType DieType { get; private set; }
}
