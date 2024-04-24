using Godot;

public enum DieType { D4, D6, D8}

public partial class Die : RigidBody3D
{
	[Export] public Sprite3D[] Faces { get; private set; }
	[Export] public DieType DieType { get; private set; } = DieType.D6;
}
