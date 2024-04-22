using Godot;
using System;
using System.Collections.Generic;

public partial class DiceManager : Node
{
	private static DiceManager instance;
	public static DiceManager Instance => instance;
	
	[Export] private PackedScene d6Debug;
	
	public override void _EnterTree()
	{
		if(instance != null)
			QueueFree();
		else
			instance = this;
	}
	
	//TODO: Will need to sync the dice roll
	public void RollDice(List<Die> dice)
	{
		RandomNumberGenerator random = new();
		
		foreach (var die in dice)
		{
			Die dieInstance = d6Debug.Instantiate<Die>();
			
			float randomX = random.RandfRange(-Mathf.Pi, Mathf.Pi);
			float randomY = random.RandfRange(-Mathf.Pi, Mathf.Pi);
			float randomZ = random.RandfRange(-Mathf.Pi, Mathf.Pi);
			
			dieInstance.Translate(new Vector3(0,5,0));
			
			dieInstance.Rotation = new Vector3(randomX, randomY, randomZ);
			
			AddChild(dieInstance);		
					
			dieInstance.ApplyCentralImpulse(dieInstance.GlobalTransform.Basis.Z * 5);
		}		
	}
}
