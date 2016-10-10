// Copyright 2011-2015 Chris Patterson, Dru Sellers
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
namespace Automatonymous.Binders
{
    using System;


    public interface EventActivityBinder<TInstance> :
        EventActivities<TInstance>
        where TInstance : class
    {
        StateMachine<TInstance> StateMachine { get; }

        Event Event { get; }

        EventActivityBinder<TInstance> Add(Activity<TInstance> activity);

        /// <summary>
        /// Catch the exception of type T, and execute the compensation chain
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="activityCallback"></param>
        /// <returns></returns>
        EventActivityBinder<TInstance> Catch<T>(
            Func<ExceptionActivityBinder<TInstance, T>, ExceptionActivityBinder<TInstance, T>> activityCallback)
            where T : Exception;
    }


    public interface EventActivityBinder<TInstance, TData> :
        EventActivities<TInstance>
        where TInstance : class
    {
        StateMachine<TInstance> StateMachine { get; }

        Event<TData> Event { get; }

        EventActivityBinder<TInstance, TData> Add(Activity<TInstance> activity);

        EventActivityBinder<TInstance, TData> Add(Activity<TInstance, TData> activity);

        /// <summary>
        /// Catch the exception of type T, and execute the compensation chain
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="activityCallback"></param>
        /// <returns></returns>
        EventActivityBinder<TInstance, TData> Catch<T>(
            Func<ExceptionActivityBinder<TInstance, TData, T>, ExceptionActivityBinder<TInstance, TData, T>> activityCallback)
            where T : Exception;
    }
}