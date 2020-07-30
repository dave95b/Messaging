using System;
using System.Collections.Generic;

namespace Messaging
{
    public abstract class Message : IMessage
    {
        public int Count { get; set; }

        public bool Equals(IMessage other) => this == other;
    }

    public class DataMessage<TData> : Message, IDataMessage<TData>
    {
        public List<TData> Data { get; } = new List<TData>();
    }

    public class CallbackMessage : Message, ICallbackMessage
    {
        public event Action OnAccepted;

        public void Dispatch() => OnAccepted?.Invoke();
    }

    public class CallbackMessage<TCallback> : Message, ICallbackMessage<TCallback>
    {
        public event Action<TCallback> OnAccepted;

        public void Dispatch(TCallback callbackData) => OnAccepted?.Invoke(callbackData);
    }

    public class RichMessage<TData> : CallbackMessage, IDataMessage<TData>
    {
        public List<TData> Data { get; } = new List<TData>();
    }

    public class RichMessage<TData, TCallback> : CallbackMessage<TCallback>, IDataMessage<TData>
    {
        public List<TData> Data { get; } = new List<TData>();
    }
}