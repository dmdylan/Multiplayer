namespace StateStuff
{
	public class RoundStartCombatState : CombatState
	{
		public RoundStartCombatState(StateMachine stateMachine, CombatManager combatManager) : base(stateMachine, combatManager)
		{
		}
		
		public override void EnterState()
		{
			base.EnterState();
			
			combatManager.StateDebugLabel.Text = "Round Start State";
		}
	}
}