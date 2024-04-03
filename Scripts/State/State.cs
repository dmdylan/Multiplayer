using Godot;
using System;

public partial class State : Node
{
	protected StateMachine stateMachine;
	
	public virtual void InitState(StateMachine stateMachine)
	{
		this.stateMachine = stateMachine;
	}
	
	public virtual void EnterState(){}
	public virtual void UpdateState(float delta){}
	public virtual void PhysicsUpdateState(float delta){}
	public virtual void ExitState(){}
}
