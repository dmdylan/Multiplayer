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
	//TODO: Dice can get stuck sometimes, might need to reroll. Can use raycast on each side to check it one is touching the ground
	public void RollDice(List<Die> dice)
	{
		RandomNumberGenerator random = new();
		
		for (int i = 0; i < dice.Count; i++)
		{
			Die die = GetDieTypeScene(dice[i]).Instantiate<Die>();
			
			die.Translate(new Vector3(random.RandfRange(-3f,3f),5, random.RandfRange(-3f,3f)));
			
			float randomX = random.RandfRange(-Mathf.Pi, Mathf.Pi);
			float randomY = random.RandfRange(-Mathf.Pi, Mathf.Pi);
			float randomZ = random.RandfRange(-Mathf.Pi, Mathf.Pi);
			
			
			die.Rotation = new Vector3(randomX, randomY, randomZ);
			
			AddChild(die);		
					
			die.ApplyCentralImpulse(die.GlobalTransform.Basis.Z * 5);
		}
		
		//TODO: Move the deletion and handling of dice out of die script and to dice manager
		//Wait for all die to finish
	}
	
	private PackedScene GetDieTypeScene(Die die)
	{
		switch (die.DieType)
		{
			case DieType.D4:
				return default;
			case DieType.D6:
				return d6Debug;
			case DieType.D8:
				return default;
			default:
				return default;
		}
	}
}
