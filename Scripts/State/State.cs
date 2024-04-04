namespace StateStuff
{
    public class State
    {
        protected StateMachine stateMachine;

        public State(StateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public virtual void EnterState() { }
        public virtual void UpdateState(float delta) { }
        public virtual void PhysicsUpdateState(float delta) { }
        public virtual void ExitState() { }
    }
}