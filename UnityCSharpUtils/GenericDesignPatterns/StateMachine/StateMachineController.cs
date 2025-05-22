using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnityUtils.GenericDesignPatterns.StateMachine
{
    public class StateMachineController
    {
        private IState _currentState;
        private readonly Dictionary<Type, IState> _states = new();
        private readonly List<AnyTransition> _anyTransitions = new();
        private readonly Dictionary<Type, List<StateTransition>> _transitions = new();

        public event Action<TransitionInfo> OnTransition;

        public void AddState<TState>(TState state) where TState : IState
        {
            _states[typeof(TState)] = state;
        }

        public void AddTransition<TFrom, TTo>(Func<bool> condition)
            where TFrom : IState
            where TTo : IState
        {
            var fromType = typeof(TFrom);
            var toType = typeof(TTo);

            if (!_transitions.ContainsKey(fromType))
            {
                _transitions[fromType] = new List<StateTransition>();
            }

            _transitions[fromType].Add(new StateTransition(toType, condition));
        }

        public void AddAnyTransition<TTo>(Func<bool> condition) where TTo : IState
        {
            _anyTransitions.Add(new AnyTransition(typeof(TTo), condition));
        }

        public void SetInitialState<T>() where T : IState
        {
            if (_states.TryGetValue(typeof(T), out var state))
            {
                _currentState = state;
                if (_currentState is IAsyncState asyncState)
                    asyncState.OnEnterAsync().Forget();
                else
                    _currentState.OnEnterState();
            }
        }

        public void ChangeState<T>() where T : IState
        {
            var newType = typeof(T);
            if (_states.TryGetValue(newType, out var newState))
            {
                TransitionTo(newState).Forget();
            }
        }

        private async UniTaskVoid TransitionTo(IState newState)
        {
            if (_currentState == newState) return;

            var from = _currentState;
            var to = newState;

            if (_currentState is IAsyncState asyncExit)
                await asyncExit.OnExitAsync();
            else
                _currentState?.OnExitState();

            _currentState = newState;

            OnTransition?.Invoke(new TransitionInfo(from?.GetType(), to.GetType()));

            if (_currentState is IAsyncState asyncEnter)
                await asyncEnter.OnEnterAsync();
            else
                _currentState.OnEnterState();
        }
    }
}
