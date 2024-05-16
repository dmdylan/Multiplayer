
using Godot;

[GlobalClass]
public partial class Spell : Resource
{
	[Export] public Texture2D Icon { get; private set; }
	[Export] public string Name { get; private set; }
}