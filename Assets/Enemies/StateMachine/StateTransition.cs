using System;

public class StateTransition {
    
    private readonly IState startState, endState;
    private readonly Func<bool> condition;

    public StateTransition(IState startState, IState endState, Func<bool> condition) {
        this.startState = startState;
        this.endState = endState;
        this.condition = condition;
    }

    public bool CheckCondition() {
        return condition.Invoke();
    }

    public IState GetStartState() {
        return startState;
    }

    public IState GetEndState() {
        return endState;
    }
}