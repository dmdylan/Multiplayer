using Godot;
using System;
using System.Collections.Generic;

public partial class DiceManager : Node
{
	private static DiceManager instance;
	public static DiceManager Instance => instance;

	[Export] private PackedScene d6Debug;

	private List<Die> currentDice = new();

	public override void _EnterTree()
	{
		if (instance != null)
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

			currentDice.Add(die);

			die.SleepingStateChanged += DieSleepingStateChangedEventHandler;
		}

		foreach (var die in currentDice)
		{
			die.Translate(new Vector3(random.RandfRange(-3f, 3f), 5, random.RandfRange(-3f, 3f)));

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

	private void DieSleepingStateChangedEventHandler()
	{
		foreach (var die in currentDice)
		{
			if (!die.Sleeping)
				return;
		}
		
		foreach (var die in currentDice)
		{
			CheckDieTopSide(die);
		}
		
		foreach (var die in currentDice)
		{
			die.SleepingStateChanged -= DieSleepingStateChangedEventHandler;
			die.QueueFree();
		}
		
		currentDice.Clear();
		//TODO: Maybe remove trimexcess
		currentDice.TrimExcess();
	}

	private void CheckDieTopSide(Die die)
	{
		float topHeight = 0;
		int index = 0;

		for (int i = 0; i < die.Faces.Length; i++)
		{
			if (die.Faces[i].GlobalTransform.Origin.Y > topHeight)
			{
				topHeight = die.Faces[i].GlobalTransform.Origin.Y;
				index = i;
			}	
		}
		
		GD.Print($"Top face is {die.Faces[index].Name}");
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
