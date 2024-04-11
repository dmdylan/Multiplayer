using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace StateStuff
{
	public class InitCombatState : CombatState
	{
		public InitCombatState(StateMachine stateMachine, CombatManager combatManager) : base(stateMachine, combatManager)
		{
		}

		public override void EnterState()
		{
			base.EnterState();

			combatManager.StateDebugLabel.Text = "Init Combat State";
			SetInitialTurnOrder();
			//Send info to other managers
			stateMachine.ChangeState("TurnSetup");
		}

		private void SetInitialTurnOrder()
		{
			var turnOrderList = combatManager.Entities.OrderByDescending(x => x.StatComponent.Stats[StatType.Speed]);
			
			foreach (var item in turnOrderList)
			{
				combatManager.CurrentTurnOrder.Add(item);
			}
		}
	}
}
