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
    }

    public class CallbackMessage<TCallback> : Message, ICallbackMessage<TCallback>
    {
        public event Action<TCallback> OnAccepted;
    }

    public class RichMessage<TData> : DataMessage<TData>, IDataMessage<TData>, ICallbackMessage
    {
        public event Action OnAccepted;
    }

    public class RichMessage<TData, TCallback> : DataMessage<TData>, IDataMessage<TData>, ICallbackMessage<TCallback>
    {
        public event Action<TCallback> OnAccepted;
    }
}