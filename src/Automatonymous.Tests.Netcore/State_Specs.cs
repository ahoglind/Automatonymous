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
    using NUnit.Framework;
    using States;


    [TestFixture]
    public class When_a_state_is_declared
    {
        class Instance
        {
            public State CurrentState { get; set; }
        }


        TestStateMachine _machine;

        [TestFixtureSetUp]
        public void A_state_is_declared()
        {
            _machine = new TestStateMachine();
        }


        class TestStateMachine :
            AutomatonymousStateMachine<Instance>
        {
            public TestStateMachine()
            {
                InstanceState(x => x.CurrentState);
            }

            public State Running { get; private set; }
        }


        [Test]
        public void It_should_capture_the_name_of_final()
        {
            Assert.AreEqual("Final", _machine.Final.Name);
        }

        [Test]
        public void It_should_capture_the_name_of_initial()
        {
            Assert.AreEqual("Initial", _machine.Initial.Name);
        }

        [Test]
        public void It_should_capture_the_name_of_running()
        {
            Assert.AreEqual("Running", _machine.Running.Name);
        }

        [Test]
        public void Should_be_an_instance_of_the_proper_type()
        {
            Assert.IsInstanceOf<StateMachineState<Instance>>(_machine.Initial);
        }
    }


    [TestFixture]
    public class When_a_state_is_stored_another_way
    {
        TestStateMachine _machine;
        Instance _instance;

        [TestFixtureSetUp]
        public void A_state_is_declared()
        {
            _machine = new TestStateMachine();
            _instance = new Instance();

            _machine.RaiseEvent(_instance, x => x.Started).Wait();
        }


        /// <summary>
        /// For this instance, the state is actually stored as a string. Therefore,
        /// it is important that the StateMachine property is initialized when the
        /// instance is hydrated, as it is used to get the State for the name of
        /// the current state. This makes it easier to save the instance using
        /// an ORM that doesn't support user types (cough, EF, cough).
        /// </summary>
        class Instance
        {
            /// <summary>
            /// The CurrentState is exposed as a string for the ORM
            /// </summary>
            public string CurrentState { get; private set; }
        }


        class TestStateMachine :
            AutomatonymousStateMachine<Instance>
        {
            public TestStateMachine()
            {
                InstanceState(x => x.CurrentState);

                Initially(
                    When(Started)
                        .TransitionTo(Running));
            }

            public Event Started { get; private set; }
            public State Running { get; private set; }
        }


        [Test]
        public void It_should_get_the_name_right()
        {
            Assert.AreEqual("Running", _instance.CurrentState);
        }
    }


    [TestFixture]
    public class When_storing_state_as_an_int
    {
        TestStateMachine _machine;
        Instance _instance;

        [TestFixtureSetUp]
        public void A_state_is_declared()
        {
            _machine = new TestStateMachine();
            _instance = new Instance();

            _machine.RaiseEvent(_instance, x => x.Started).Wait();
        }


        /// <summary>
        /// For this instance, the state is actually stored as a string. Therefore,
        /// it is important that the StateMachine property is initialized when the
        /// instance is hydrated, as it is used to get the State for the name of
        /// the current state. This makes it easier to save the instance using
        /// an ORM that doesn't support user types (cough, EF, cough).
        /// </summary>
        class Instance
        {
            /// <summary>
            /// The CurrentState is exposed as a string for the ORM
            /// </summary>
            public int CurrentState { get; private set; }
        }


        class TestStateMachine :
            AutomatonymousStateMachine<Instance>
        {
            public TestStateMachine()
            {
                InstanceState(x => x.CurrentState, Running);

                Initially(
                    When(Started)
                        .TransitionTo(Running));
            }

            public Event Started { get; private set; }
            public State Running { get; private set; }
        }


        [Test]
        public void It_should_get_the_name_right()
        {
            Assert.AreEqual(_machine.Running, _machine.GetState(_instance).Result);
        }
    }
}