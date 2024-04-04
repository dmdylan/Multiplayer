namespace StateStuff
{
	public class CombatState : State
	{
		protected CombatManager combatManager;
		
		public CombatState(StateMachine stateMachine, CombatManager combatManager) : base(stateMachine)
		{
			this.combatManager = combatManager;
		}
	}
}