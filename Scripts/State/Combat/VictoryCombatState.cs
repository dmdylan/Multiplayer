using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StateStuff
{
	public class VictoryCombatState : CombatState
	{
		public VictoryCombatState(StateMachine stateMachine, CombatManager combatManager) : base(stateMachine, combatManager)
		{
		}
		
		public override void EnterState()
		{
			base.EnterState();
			
			combatManager.StateDebugLabel.Text = "Victory State";
		}
	}
}