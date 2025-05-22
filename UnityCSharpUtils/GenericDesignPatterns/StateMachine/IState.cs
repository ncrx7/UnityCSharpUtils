using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityUtils.GenericDesignPatterns.StateMachine
{
    public interface IState 
    {
        public void OnEnterState();
        
        public void Tick();

        public void OnExitState();
    }
}
