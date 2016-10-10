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
    using System.Linq;
    using System.Threading.Tasks;
    using NUnit.Framework;
#if NETSTANDARD || NETCORE
  using System.Threading.Tasks;
#endif

  [TestFixture]
    public class Raising_an_unhandled_event_in_a_state
    {
        [Test]
#if NETSTANDARD || NETCORE
    public async Task Should_throw_an_exception_when_event_is_not_allowed_in_current_state()
#else
        public async void Should_throw_an_exception_when_event_is_not_allowed_in_current_state()
#endif
        {
            var instance = new Instance();

            await _machine.RaiseEvent(instance, x => x.Start);

      #if NETSTANDARD || NETCORE
      var catched = false;
      try {
        await _machine.RaiseEvent(instance, x => x.Start);
      } catch (UnhandledEventException) {
        catched = true;
      }
      Assert.IsTrue(catched);
#else
            Assert.Throws<UnhandledEventException>(async () => await _machine.RaiseEvent(instance, x => x.Start));
#endif
        }

        TestStateMachine _machine;


        class Instance
        {
            public State CurrentState { get; set; }
        }


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
                Initially(
                    When(Start)
                        .TransitionTo(Running));
            }

            public Event Start { get; private set; }

            public State Running { get; private set; }
        }
    }

    [TestFixture]
    public class Raising_an_ignored_event_that_is_not_filtered
    {
        [Test]
#if NETSTANDARD || NETCORE
    public async Task Should_throw_an_exception_when_event_is_not_allowed_in_current_state()
#else
        public async void Should_throw_an_exception_when_event_is_not_allowed_in_current_state()
#endif
        {
            var instance = new Instance();

            await _machine.RaiseEvent(instance, x => x.Start);

#if NETSTANDARD || NETCORE
      var catched = false;
      try {
        await _machine.RaiseEvent(instance, x => x.Charge, new A { Volts = 12 });
      } catch (UnhandledEventException) {
        catched = true;
      }
      Assert.IsTrue(catched);
#else

            Assert.Throws<UnhandledEventException>(async () => await _machine.RaiseEvent(instance, x => x.Charge, new A { Volts = 12 }));
#endif
        }

        TestStateMachine _machine;


        class Instance
        {
            public State CurrentState { get; set; }
            public int Volts { get; set; }
        }


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
                Initially(
                    When(Start)
                        .TransitionTo(Running));

                During(Running,
                    Ignore(Start),
                    Ignore(Charge, x => x.Data.Volts == 9));
            }

            public Event Start { get; private set; }
            public Event<A> Charge { get; private set; }

            public State Running { get; private set; }
        }


        class A
        {
            public int Volts { get; set; }
        }

    }


    [TestFixture]
    public class Raising_an_ignored_event
    {
        [Test]
        public async Task Should_also_ignore_yet_process_invalid_events()
        {
            var instance = new Instance();

            await _machine.RaiseEvent(instance, x => x.Start);

            await _machine.RaiseEvent(instance, x => x.Charge, new A {Volts = 12});

            Assert.AreEqual(0, instance.Volts);
        }

        [Test]
        public async Task Should_silenty_ignore_the_invalid_event()
        {
            var instance = new Instance();

            await _machine.RaiseEvent(instance, x => x.Start);

            await _machine.RaiseEvent(instance, x => x.Start);
        }

        [Test]
        public async Task Should_have_the_next_event_even_though_ignored()
        {
            var instance = new Instance();

            await _machine.RaiseEvent(instance, x => x.Start);

            Assert.AreEqual(_machine.Running, await _machine.GetState(instance));

            var nextEvents = await _machine.NextEvents(instance);

            Assert.IsTrue(nextEvents.Any(x => x.Name.Equals("Charge")));
        }

        TestStateMachine _machine;


        class Instance
        {
            public State CurrentState { get; set; }
            public int Volts { get; set; }
        }


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
                Initially(
                    When(Start)
                        .TransitionTo(Running));

                During(Running,
                    Ignore(Start),
                    Ignore(Charge));
            }

            public Event Start { get; private set; }
            public Event<A> Charge { get; private set; }

            public State Running { get; private set; }
        }


        class A
        {
            public int Volts { get; set; }
        }
    }

    [TestFixture]
    public class Raising_an_unhandled_event_when_the_state_machine_ignores_all_unhandled_events
    {
        [Test]
#if NETSTANDARD || NETCORE
    public async Task Should_silenty_ignore_the_invalid_event()
#else
        public async void Should_silenty_ignore_the_invalid_event()
#endif
        {
            var instance = new Instance();

            await _machine.RaiseEvent(instance, x => x.Start);

            await _machine.RaiseEvent(instance, x => x.Start);
        }

        TestStateMachine _machine;


        class Instance
        {
            public State CurrentState { get; set; }
        }


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
                Event(() => Start);

                State(() => Running);

                OnUnhandledEvent(x => x.Ignore());

                Initially(
                    When(Start)
                        .TransitionTo(Running));
            }

            public Event Start { get; private set; }

            public State Running { get; private set; }
        }
    }
}