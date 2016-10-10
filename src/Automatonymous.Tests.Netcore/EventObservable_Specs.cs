﻿// Copyright 2011-2015 Chris Patterson, Dru Sellers
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
namespace Automatonymous.Tests
{
    using System;
    using NUnit.Framework;


    [TestFixture]
    public class When_an_event_is_raised_on_an_instance
    {
        Instance _instance;
        InstanceStateMachine _machine;
        EventRaisedObserver<Instance> _observer;

        [TestFixtureSetUp]
        public void Specifying_an_event_activity()
        {
            _instance = new Instance();
            _machine = new InstanceStateMachine();
            _observer = new EventRaisedObserver<Instance>();

            using (IDisposable subscription = _machine.ConnectEventObserver(_machine.Initialized, _observer))
                _machine.RaiseEvent(_instance, x => x.Initialized).Wait();
        }


        class Instance
        {
            public State CurrentState { get; set; }
        }


        class InstanceStateMachine :
            AutomatonymousStateMachine<Instance>
        {
            public InstanceStateMachine()
            {
                During(Initial,
                    When(Initialized)
                        .TransitionTo(Running));
            }

            public State Running { get; private set; }
            public Event Initialized { get; private set; }
        }


        [Test]
        public void Should_have_raised_the_initialized_event()
        {
            Assert.AreEqual(_machine.Initialized, _observer.Events[0].Event);
        }

        [Test]
        public void Should_raise_the_event()
        {
            Assert.AreEqual(1, _observer.Events.Count);
        }
    }
}