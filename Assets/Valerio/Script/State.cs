using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiniteStateMachine
{
    public enum StateType
    {
        IDLE
    }

    public class State : ScriptableObject
    {
        protected StateMachine stateMachine;
        public StateType GoTo;
        public void SetStateMachine(StateMachine _stateMachine)
        {
            stateMachine = _stateMachine;
        }

        public virtual void OnUpdate() { ; }
        public virtual void OnEnter() { ; }
        public virtual void OnExit() { ; }
    }
}

    


