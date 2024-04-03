using Godot;
using System;
using System.Collections.Generic;

public partial class StateMachine : Node
{	
	[Export] private NodePath initalState;
	
	private Dictionary<string, State> states = new();
	private State currentState;
	
	public void InitStateMachine(State state)
	{
		foreach (Node node in GetChildren())
		{
			if(node is State s)
			{
				states.Add(s.Name, s);
				s.InitState(this);
			}
		}
		
		currentState = state;
		currentState.EnterState();
	}
	
	public void ChangeState(State state)
	{
		if(!states.ContainsKey(state.Name) || currentState == state)
			return;
		
		currentState.ExitState();
		
		currentState = states[state.Name];
		
		currentState.EnterState();
	}
}
