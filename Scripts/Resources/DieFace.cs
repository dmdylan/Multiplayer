using Godot;

[GlobalClass]
public partial class DieFace : Resource
{
	[Export] public Image Image { get; private set; }
	[Export] public string Name { get; private set;}
	[Export] public int Value { get; private set; }
}
