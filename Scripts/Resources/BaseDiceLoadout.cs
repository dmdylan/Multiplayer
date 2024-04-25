using Godot;

[GlobalClass]
public partial class BaseDiceLoadout : Resource
{
	//TODO: Need class that has image and type reference? Could use enums maybe?
	[Export] public Die[] Dice { get; private set; }
}