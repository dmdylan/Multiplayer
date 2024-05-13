using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class DiceManager : Node
{
	private static DiceManager instance;
	public static DiceManager Instance => instance;

	[Export] private PackedScene d6Debug;

	private List<Die> currentDice = new();

	private MultiplayerSynchronizer multiplayerSynchronizer;

	public override void _EnterTree()
	{
		if (instance != null)
			QueueFree();
		else
			instance = this;
	}

	public void RollDice(int id)
	{
		// if(Multiplayer.IsServer())
		// 	RollDiceRpc(id);
		// else
			Rpc(nameof(RollDiceRpc), id);
	}

	//FIXME: Dice can get stuck sometimes, might need to reroll. Can use raycast on each side to check it one is touching the ground
	//FIXME: Dice are synched through multiplayer synchronizer and 0 delay, could be heavy on network. Switch to lerp positions?
	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]
	private void RollDiceRpc(int entityID)
	{
		ClearDiceList();
		
		Entity entity = EntityManager.Instance.ActiveEntities.Where(x => x.EntityListID == entityID).First();

		foreach (var die in entity.DiceComponent.DiceList)
		{
			Die dieInstance = GetDieTypeScene(die).Instantiate<Die>();
			
			dieInstance.InitDie(die.DieInfo);
			
			currentDice.Add(dieInstance);
			
			dieInstance.SleepingStateChanged += DieSleepingStateChangedEventHandler;
		}
		
		RandomNumberGenerator random = new();

		foreach (var die in currentDice)
		{
			die.Translate(new Vector3(random.RandfRange(-3f, 3f), 5, random.RandfRange(-3f, 3f)));

			float randomX = random.RandfRange(-Mathf.Pi, Mathf.Pi);
			float randomY = random.RandfRange(-Mathf.Pi, Mathf.Pi);
			float randomZ = random.RandfRange(-Mathf.Pi, Mathf.Pi);

			die.Rotation = new Vector3(randomX, randomY, randomZ);

			AddChild(die);
						
			die.ApplyCentralImpulse(die.GlobalTransform.Basis.Z * 20);	
		}
	}

	[Rpc(MultiplayerApi.RpcMode.AnyPeer, CallLocal = true)]
	private void SpawnDice()
	{
		foreach (var die in currentDice)
		{
			AddChild(die);
						
			die.ApplyCentralImpulse(die.GlobalTransform.Basis.Z * 20);	
		}
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
	}

	private void ClearDiceList()
	{
		if(currentDice == null || currentDice.Count == 0)
			return;
		
		foreach (var die in currentDice)
		{
			die.SleepingStateChanged -= DieSleepingStateChangedEventHandler;
			die.QueueFree();
		}
		
		currentDice.Clear();
		currentDice.TrimExcess();
	}

	private void CheckDieTopSide(Die die)
	{
		float topHeight = float.MinValue;
		int index = 0;

		for (int i = 0; i < die.FaceImages.Length; i++)
		{
			if (die.FaceImages[i].GlobalTransform.Origin.Y > topHeight)
			{
				topHeight = die.FaceImages[i].GlobalTransform.Origin.Y;
				index = i;
			}	
		}

		GD.Print($"Top face is {die.DieFaces[index].Name}");
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
