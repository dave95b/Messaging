using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Messaging
{
    public interface IMessageBox
    {
        void SendSimple<TMessage>()
            where TMessage : IMessage, new();
        void SendWithData<TMessage, TData>(TData data)
            where TMessage : IMessage<TData>, new();
        void SendWithCallback<TMessage>(Action callback)
            where TMessage : ICallbackMessage, new();
        void SendWithCallback<TMessage, TCallback>(Action<TCallback> data)
            where TMessage : ICallbackMessage<TCallback>, new();
        void SendRich<TMessage, TData>(TData data, Action callback)
            where TMessage : IMessage<TData>, ICallbackMessage, new();
        void SendRich<TMessage, TData, TCallback>(TData data, Action<TCallback> callback)
            where TMessage : IMessage<TData>, ICallbackMessage<TCallback>, new();
    }

    public interface IReadableMessageBox : IMessageBox
    {
        event Action<IMessage> OnReceived;

        TMessage Peek<TMessage>()
            where TMessage : IMessage;
        TMessage Get<TMessage>()
            where TMessage : IMessage;
    }

    internal class MessageBox : IReadableMessageBox
    {
        public event Action<IMessage> OnReceived;

        private readonly Dictionary<Type, IMessage> messages;


        public TMessage Get<TMessage>() where TMessage : IMessage
        {
            throw new NotImplementedException();
        }

        public TMessage Peek<TMessage>() where TMessage : IMessage
        {
            throw new NotImplementedException();
        }

        public void SendSimple<TMessage>() where TMessage : IMessage, new()
        {
            throw new NotImplementedException();
        }

        public void SendWithData<TMessage, TData>(TData data) where TMessage : IMessage<TData>, new()
        {
            throw new NotImplementedException();
        }

        public void SendWithCallback<TMessage>(Action callback) where TMessage : ICallbackMessage, new()
        {
            throw new NotImplementedException();
        }

        public void SendWithCallback<TMessage, TCallback>(Action<TCallback> data) where TMessage : ICallbackMessage<TCallback>, new()
        {
            throw new NotImplementedException();
        }

        public void SendRich<TMessage, TData>(TData data, Action callback) where TMessage : IMessage<TData>, ICallbackMessage, new()
        {
            throw new NotImplementedException();
        }

        public void SendRich<TMessage, TData, TCallback>(TData data, Action<TCallback> callback) where TMessage : IMessage<TData>, ICallbackMessage<TCallback>, new()
        {
            throw new NotImplementedException();
        }
    }
}