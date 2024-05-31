using UnityEngine;


namespace DesignPatterns.FiniteStateMachine
{
    public abstract class FiniteStateMachine
    {
        protected State currentState;

        public virtual void Initialization(State initialState)
        {
            if (initialState != null)
            {
                this.currentState = initialState;
                this.currentState.Enter();
            }

        }

        public virtual void ChangeState(State newState)
        {
            this.currentState.Exit();
            this.currentState = newState;
            this.currentState.Enter();
        }

        public virtual void UpdateState()
        {
            this.currentState.Update();
        }

    }


    public abstract class State
    {
        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }

}

