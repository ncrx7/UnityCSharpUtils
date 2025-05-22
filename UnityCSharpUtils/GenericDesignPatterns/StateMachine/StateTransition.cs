using System;

namespace UnityUtils.GenericDesignPatterns.StateMachine
{
    public class StateTransition
    {
        public Type ToStateType { get; }
        public Func<bool> Condition { get; }

        public StateTransition(Type toStateType, Func<bool> condition)
        {
            ToStateType = toStateType;
            Condition = condition;
        }
    }

    public class AnyTransition
    {
        public Type ToStateType { get; }
        public Func<bool> Condition { get; }

        public AnyTransition(Type toStateType, Func<bool> condition)
        {
            ToStateType = toStateType;
            Condition = condition;
        }
    }

    public readonly struct TransitionInfo
    {
        public Type From { get; }
        public Type To { get; }

        public TransitionInfo(Type from, Type to)
        {
            From = from;
            To = to;
        }
    }
}
