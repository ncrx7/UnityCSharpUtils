using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp;
using Cysharp.Threading.Tasks;

namespace UnityUtils.GenericDesignPatterns.Command
{
    public interface ICommand<T>
    {
        public UniTask ExecuteCommand(T arg);
    }
}
