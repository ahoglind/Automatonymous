// Copyright 2011-2014 Chris Patterson, Dru Sellers
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
namespace Automatonymous
{
    using Lifts;


    public static class InstanceLiftExtensions
    {
        public static InstanceLift<T> CreateInstanceLift<T, TInstance>(this T stateMachine, TInstance instance)
            where T : class, StateMachine, StateMachine<TInstance>
            where TInstance : class
        {
            var instanceLift = new StateMachineInstanceLift<T, TInstance>(stateMachine, instance);

            return instanceLift;
        }
    }
}