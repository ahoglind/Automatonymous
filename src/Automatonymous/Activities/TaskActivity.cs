﻿// Copyright 2011-2014 Chris Patterson, Dru Sellers
// 
// Licensed under the Apache License, Version 2.0 (the "License"); you may not use 
// this file except in compliance with the License. You may obtain a copy of the 
// License at 
// 
//     http://www.apache.org/licenses/LICENSE-2.0 
// 
// Unless required by applicable law or agreed to in writing, software distributed 
// under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR 
// CONDITIONS OF ANY KIND, either express or implied. See the License for the 
// specific language governing permissions and limitations under the License.
namespace Automatonymous.Activities
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;


    public class TaskActivity<TInstance> :
        Activity<TInstance>
    {
        readonly Func<TInstance, CancellationToken, Task> _action;

        public TaskActivity(Func<TInstance, CancellationToken, Task> action)
        {
            _action = action;
        }

        void AcceptStateMachineInspector.Accept(StateMachineInspector inspector)
        {
            inspector.Inspect(this, x => { });
        }

        async Task Activity<TInstance>.Execute(TInstance instance, CancellationToken cancellationToken)
        {
            await _action(instance, cancellationToken);
        }

        async Task Activity<TInstance>.Execute<T>(TInstance instance, T value, CancellationToken cancellationToken)
        {
            await _action(instance, cancellationToken);
        }
    }


    public class TaskActivity<TInstance, TData> :
        Activity<TInstance, TData>
    {
        readonly Func<TInstance, TData, CancellationToken, Task> _action;

        public TaskActivity(Func<TInstance, TData, CancellationToken, Task> action)
        {
            _action = action;
        }

        void AcceptStateMachineInspector.Accept(StateMachineInspector inspector)
        {
            inspector.Inspect(this, x => { });
        }

        async Task Activity<TInstance, TData>.Execute(TInstance instance, TData value, CancellationToken cancellationToken)
        {
            await _action(instance, value, cancellationToken);
        }
    }
}