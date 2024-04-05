using Godot;
using StateStuff;
using System;

public partial class CombatManager : Node
{
	private readonly StateMachine stateMachine = new();

	public override void _Ready()
	{
		base._Ready();
		
		stateMachine.AddState("BattleStart", new BattleStartCombatState(stateMachine, this));
		stateMachine.AddState("RoundStart", new RoundStartCombatState(stateMachine, this));
		stateMachine.AddState("RoundEnd", new RoundEndCombatState(stateMachine, this));
		stateMachine.AddState("TurnInterlude", new TurnInterludeCombatState(stateMachine, this));
		stateMachine.AddState("PlayerTurn", new PlayerTurnCombatState(stateMachine, this));
		stateMachine.AddState("EnemyTurn", new EnemyTurnCombatState(stateMachine, this));
		stateMachine.AddState("Victory", new VictoryCombatState(stateMachine, this));
		stateMachine.AddState("Defeat", new DefeatCombatState(stateMachine, this));
		
		stateMachine.InitStateMachine("BattleStart");
	}
}
