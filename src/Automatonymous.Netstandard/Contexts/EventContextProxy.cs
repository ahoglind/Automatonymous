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
namespace Automatonymous.Contexts
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;


    public class EventContextProxy<TInstance> :
        EventContext<TInstance>,
        IDisposable
    {
        readonly CancellationTokenSource _cancellationTokenSource;
        readonly EventContext<TInstance> _context;
        CancellationTokenRegistration _contextRegistration;
        CancellationTokenRegistration _registration;

        public EventContextProxy(EventContext<TInstance> context, Event @event, CancellationToken cancellationToken)
        {
            _context = context;
            Event = @event;

            _cancellationTokenSource = new CancellationTokenSource();
            _registration = cancellationToken.Register(() => _cancellationTokenSource.Cancel());
            _contextRegistration = context.CancellationToken.Register(() => _cancellationTokenSource.Cancel());
        }

        public bool HasPayloadType(Type contextType)
        {
            return _context.HasPayloadType(contextType);
        }

        public bool TryGetPayload<TPayload>(out TPayload payload) where TPayload : class
        {
            return _context.TryGetPayload(out payload);
        }

        public TPayload GetOrAddPayload<TPayload>(PayloadFactory<TPayload> payloadFactory) where TPayload : class
        {
            return _context.GetOrAddPayload(payloadFactory);
        }

        public Task Raise(Event @event, CancellationToken cancellationToken = new CancellationToken())
        {
            return _context.Raise(@event, cancellationToken);
        }

        public Task Raise<T>(Event<T> @event, T data, CancellationToken cancellationToken = new CancellationToken())
        {
            return _context.Raise(@event, data, cancellationToken);
        }

        public CancellationToken CancellationToken => _cancellationTokenSource.Token;
        public Event Event { get; }
        public TInstance Instance => _context.Instance;

        public void Dispose()
        {
            _contextRegistration.Dispose();
            _registration.Dispose();
            _cancellationTokenSource.Dispose();
        }
    }


    public class EventContextProxy<TInstance, TData> :
        EventContext<TInstance, TData>,
        IDisposable
    {
        readonly CancellationTokenSource _cancellationTokenSource;
        readonly EventContext<TInstance> _context;
        readonly Event<TData> _event;
        CancellationTokenRegistration _contextRegistration;
        CancellationTokenRegistration _registration;

        public EventContextProxy(EventContext<TInstance> context, Event<TData> @event, TData data, CancellationToken cancellationToken)
        {
            _context = context;
            _event = @event;
            Data = data;

            _cancellationTokenSource = new CancellationTokenSource();
            _registration = cancellationToken.Register(() => _cancellationTokenSource.Cancel());
            _contextRegistration = context.CancellationToken.Register(() => _cancellationTokenSource.Cancel());
        }

        public bool HasPayloadType(Type contextType)
        {
            return _context.HasPayloadType(contextType);
        }

        public bool TryGetPayload<TPayload>(out TPayload payload) where TPayload : class
        {
            return _context.TryGetPayload(out payload);
        }

        public TPayload GetOrAddPayload<TPayload>(PayloadFactory<TPayload> payloadFactory) where TPayload : class
        {
            return _context.GetOrAddPayload(payloadFactory);
        }

        public Task Raise(Event @event, CancellationToken cancellationToken = new CancellationToken())
        {
            return _context.Raise(@event, cancellationToken);
        }

        public Task Raise<T>(Event<T> @event, T data, CancellationToken cancellationToken = new CancellationToken())
        {
            return _context.Raise(@event, data, cancellationToken);
        }

        public TData Data { get; }
        public CancellationToken CancellationToken => _cancellationTokenSource.Token;
        Event EventContext<TInstance>.Event => _event;
        Event<TData> EventContext<TInstance, TData>.Event => _event;
        public TInstance Instance => _context.Instance;

        public void Dispose()
        {
            _contextRegistration.Dispose();
            _registration.Dispose();
            _cancellationTokenSource.Dispose();
        }
    }
}