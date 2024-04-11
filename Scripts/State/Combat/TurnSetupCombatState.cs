using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StateStuff
{
	public class TurnSetupCombatState : CombatState
	{
		public TurnSetupCombatState(StateMachine stateMachine, CombatManager combatManager) : base(stateMachine, combatManager)
		{
		}
		
		public override void EnterState()
		{
			base.EnterState();
			
			combatManager.StateDebugLabel.Text = "Turn Setup State";
			
			SetNextTurn();
		}
		
		private void SetNextTurn()
		{
			if(GameManager.Instance.Players.Select(x => x.PlayerEntity == combatManager.CurrentTurnOrder[0]).FirstOrDefault())
			{
				stateMachine.ChangeState("PlayerTurn");
			}
			else
			{
				stateMachine.ChangeState("EnemyTurn");
			}
		}
	}
}