using Godot;

[GlobalClass]
public partial class EntityDatabase : Resource
{
	[Export] public EntityInfo[] Entities { get; private set; }
}
