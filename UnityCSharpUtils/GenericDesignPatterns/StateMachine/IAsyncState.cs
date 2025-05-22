using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnityUtils.GenericDesignPatterns.StateMachine
{
    public interface IAsyncState : IState
    {
        public UniTask OnEnterAsync();
        public UniTask OnExitAsync();
    }
}
