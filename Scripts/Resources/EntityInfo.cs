using Godot;
using System;

[GlobalClass]
public partial class EntityInfo : Resource
{
	[Export] public string Name { get; private set; }
	[Export] public Image EntityIcon { get; private set; }
	[Export] public BaseStatSheet BaseStatSheet { get; private set; }
}