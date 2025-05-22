using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UnityUtils.GenericDesignPatterns.Command
{
    public class CommandManager<T>
    {
        private Queue<ICommand<T>> _commandQueue = new();
        public T Arg;

        public void AddCommandToQueue(ICommand<T> command)
        {
            _commandQueue.Enqueue(command);
        }

        public async UniTask RunQueueCommands()
        {
            while (_commandQueue.Count > 0)
            {
                var command = _commandQueue.Dequeue();
                await command.ExecuteCommand(Arg);
            }

            //todo..
        }
    }
}
