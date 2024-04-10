using System.Collections.Generic;

namespace StateStuff
{
    public class StateMachine
    {
        private Dictionary<string, State> states = new();
        public State CurrentState { get; private set; }
        public string CurrentStateName { get; private set; }
        public string PreviousStateName { get; private set; }

        public void AddState(string key, State state)
        {
            states.Add(key, state);
        }

        public void InitStateMachine(string stateName)
        {
            CurrentState = states[stateName];
            CurrentState.EnterState();
        }

        public void InvokeUpdateState(float delta) => CurrentState.UpdateState(delta);
        public void InvokePhysicsUpdateState(float delta) => CurrentState.PhysicsUpdateState(delta);

        public void ChangeState(string stateName)
        {
            if (!states.ContainsKey(stateName) || CurrentState == states[stateName])
                return;

            CurrentState.ExitState();

            CurrentState = states[stateName];
            CurrentStateName = stateName;

            CurrentState.EnterState();
        }
    }
}