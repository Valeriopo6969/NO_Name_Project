using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FiniteStateMachine
{
    public class StateMachine : MonoBehaviour
    {
        [System.Serializable]
        public struct KeyValuePair
        {
            public StateType key;
            public State val;
        }

        public List<KeyValuePair> MyStates;

        public Dictionary<StateType, State> states;

        public StateType StartingState;

        private State currentState;

        private void Start()
        {
            states = new Dictionary<StateType, State>();
            SetStates();
            currentState = states[StartingState];
        }


        private void Update()
        {
            currentState?.OnUpdate();
        }

        public void GoTo(StateType _stateType)
        {
            currentState?.OnExit();

            currentState = states[_stateType];
            try
            {
                currentState.OnEnter();
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e);
            }
        }

        private void SetStates()
        {
            foreach (var _states in MyStates)
            {
                states.Add(_states.key, _states.val);
                states[_states.key].SetStateMachine(this);
            }
        }
    }
}        
    



