
using System.Collections.Generic;
using Godot;

[GlobalClass]
public partial class SpellInfo : Resource
{
	[Export] public Texture2D Icon { get; private set; }
	[Export] public string Name { get; private set; }
	// [Export] public List<Power> Power { get; private set; }
}