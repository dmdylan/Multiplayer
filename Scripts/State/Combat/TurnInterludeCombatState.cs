using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StateStuff
{
    public class TurnInterludeCombatState : CombatState
    {
        public TurnInterludeCombatState(StateMachine stateMachine, CombatManager combatManager) : base(stateMachine, combatManager)
        {
        }
    }
}