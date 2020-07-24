using System;
using System.Collections.Generic;

namespace Messaging
{
    public interface IMessageBox
    {
        void SendSimple<TMessage>()
            where TMessage : IMessage, new();
        void SendWithData<TMessage, TData>(TData data)
            where TMessage : IDataMessage<TData>, new();
        void SendWithCallback<TMessage>(Action callback)
            where TMessage : ICallbackMessage, new();
        void SendWithCallback<TMessage, TCallback>(Action<TCallback> callback)
            where TMessage : ICallbackMessage<TCallback>, new();
        void SendRich<TMessage, TData>(TData data, Action callback)
            where TMessage : IDataMessage<TData>, ICallbackMessage, new();
        void SendRich<TMessage, TData, TCallback>(TData data, Action<TCallback> callback)
            where TMessage : IDataMessage<TData>, ICallbackMessage<TCallback>, new();
    }

    public interface IReadableMessageBox : IMessageBox
    {
        event Action<IMessage> OnReceived;

        TMessage Peek<TMessage>()
            where TMessage : IMessage;
        TMessage Get<TMessage>()
            where TMessage : IMessage;
        void Remove<TMessage>()
            where TMessage : IMessage;
        void Clear();
    }

    internal class MessageBox : IReadableMessageBox
    {
        public event Action<IMessage> OnReceived;

        private readonly Dictionary<Type, IMessage> messages;


        public TMessage Get<TMessage>()
            where TMessage : IMessage
        {
            var message = Peek<TMessage>();
            Remove<TMessage>();

            return message;
        }

        public TMessage Peek<TMessage>() 
            where TMessage : IMessage
        {
            return messages.TryGetValue(typeof(TMessage), out var message) ? (TMessage)message : default;
        }

        public void Remove<TMessage>()
            where TMessage : IMessage => messages.Remove(typeof(TMessage));

        public void Clear() => messages.Clear();

        public void SendSimple<TMessage>()
            where TMessage : IMessage, new()
        {
            DoSend<TMessage>();
        }

        public void SendWithData<TMessage, TData>(TData data) 
            where TMessage : IDataMessage<TData>, new()
        {
            var message = DoSend<TMessage>();
            message.Data.Add(data);
        }

        public void SendWithCallback<TMessage>(Action callback)
            where TMessage : ICallbackMessage, new()
        {
            var message = DoSend<TMessage>();
            message.OnAccepted += callback;
        }

        public void SendWithCallback<TMessage, TCallback>(Action<TCallback> callback) 
            where TMessage : ICallbackMessage<TCallback>, new()
        {
            var message = DoSend<TMessage>();
            message.OnAccepted += callback;
        }

        public void SendRich<TMessage, TData>(TData data, Action callback) 
            where TMessage : IDataMessage<TData>, ICallbackMessage, new()
        {
            var message = DoSend<TMessage>();
            message.Data.Add(data);
            message.OnAccepted += callback;
        }

        public void SendRich<TMessage, TData, TCallback>(TData data, Action<TCallback> callback)
            where TMessage : IDataMessage<TData>, ICallbackMessage<TCallback>, new()
        {
            var message = DoSend<TMessage>();
            message.Data.Add(data);
            message.OnAccepted += callback;
        }

        private TMessage DoSend<TMessage>()
            where TMessage : IMessage, new()
        {
            var message = Peek<TMessage>();

            if (message.Equals(default))
            {
                message = new TMessage();
                messages[typeof(TMessage)] = message;
            }

            message.Count++;

            return message;
        }
    }
}