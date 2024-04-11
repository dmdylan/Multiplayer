using Godot;
using StateStuff;
using System.Collections.Generic;
using System.Linq;

public partial class CombatManager : Node
{
	[Export] public Label StateDebugLabel { get; private set; }
	
	//Main entity lists
	public List<Entity> Entities { get; private set; } = new();
	public List<Entity> FriendlyEntities { get; private set; } = new();
	public List<Entity> EnemyEntities { get; private set; } = new();
	
	//Turn order lists
	public List<Entity> CurrentTurnOrder { get; private set; } = new();
	public List<Entity> NextTurnOrder { get; private set; } = new();
	
	private readonly StateMachine stateMachine = new();

	public override void _Ready()
	{
		base._Ready();
		
		stateMachine.AddState("InitCombat", new InitCombatState(stateMachine, this));
		stateMachine.AddState("BattleStart", new BattleStartCombatState(stateMachine, this));
		stateMachine.AddState("RoundStart", new RoundStartCombatState(stateMachine, this));
		stateMachine.AddState("RoundEnd", new RoundEndCombatState(stateMachine, this));
		stateMachine.AddState("TurnSetup", new TurnSetupCombatState(stateMachine, this));
		stateMachine.AddState("PlayerTurn", new PlayerTurnCombatState(stateMachine, this));
		stateMachine.AddState("EnemyTurn", new EnemyTurnCombatState(stateMachine, this));
		stateMachine.AddState("Victory", new VictoryCombatState(stateMachine, this));
		stateMachine.AddState("Defeat", new DefeatCombatState(stateMachine, this));
		
		stateMachine.InitStateMachine("InitCombat");
	}
}
