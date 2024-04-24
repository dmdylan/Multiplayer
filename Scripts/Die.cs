using Godot;

public enum DieType { D4, D6, D8}

public partial class Die : RigidBody3D
{
	[Export] private Sprite3D[] faces;
	[Export] public DieType DieType { get; private set; } = DieType.D6;

	private bool hasCheckedSides = false;

	public override void _EnterTree()
	{
		SleepingStateChanged += SleepingStateChangedEventHandler;
	}

	public override void _ExitTree()
	{
		SleepingStateChanged -= SleepingStateChangedEventHandler;
	}

	//TODO: Can just move all of this logic to the dicemanager, let it check there and just use Die as a fancy data container possibly
	private async void SleepingStateChangedEventHandler()
	{
		float topHeight = 0;
		int index = 0;

		for (int i = 0; i < faces.Length; i++)
		{
			if (faces[i].GlobalTransform.Origin.Y > topHeight)
			{
				topHeight = faces[i].GlobalTransform.Origin.Y;
				index = i;
			}
		}

		GD.Print(index);
		hasCheckedSides = true;
		GD.Print($"Top face is {faces[index].Name}");

		await ToSignal(GetTree().CreateTimer(.5f), "timeout");

		QueueFree();
	}
}
