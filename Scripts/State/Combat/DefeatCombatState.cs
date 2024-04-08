using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StateStuff
{
	public class DefeatCombatState : CombatState
	{
		public DefeatCombatState(StateMachine stateMachine, CombatManager combatManager) : base(stateMachine, combatManager)
		{
		}

        public override void EnterState()
		{
			base.EnterState();
			
			combatManager.StateDebugLabel.Text = "Defeat State";
		}
    }
}