using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StateStuff
{
	public class EnemyTurnCombatState : CombatState
	{
		public EnemyTurnCombatState(StateMachine stateMachine, CombatManager combatManager) : base(stateMachine, combatManager)
		{
		}
		
		public override void EnterState()
		{
			base.EnterState();
			
			combatManager.StateDebugLabel.Text = "Enemy Turn State";
		}
	}
}