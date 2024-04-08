namespace StateStuff
{
	public class BattleStartCombatState : CombatState
	{
		public BattleStartCombatState(StateMachine stateMachine, CombatManager combatManager) : base(stateMachine, combatManager)
		{
		}

		public override void EnterState()
		{
			base.EnterState();
			
			combatManager.StateDebugLabel.Text = "Battle Start State";
		}
	}
}