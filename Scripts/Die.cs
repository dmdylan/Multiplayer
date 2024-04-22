using Godot;

public partial class Die : RigidBody3D
{
	[Export] private Sprite3D[] faces;
	
	private bool hasCheckedSides = false;
	
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		
		if(Sleeping && !hasCheckedSides)
		{
			float topHeight = 0;
			int index = 0;
			
			for (int i = 0; i < faces.Length; i++)
			{				
				if(faces[i].GlobalTransform.Origin.Y > topHeight)
				{
					topHeight = faces[i].GlobalTransform.Origin.Y;
					index = i;
				}			
			}
			
			GD.Print(index);
			hasCheckedSides = true;
			GD.Print($"Top face is {faces[index].Name}");
		}
	}
}
