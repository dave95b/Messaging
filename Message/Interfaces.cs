using System;
using System.Collections.Generic;

namespace Messaging
{
    public interface IMessage : IEquatable<IMessage>
    {
        int Count { get; set; }
    }

    public interface IDataMessage<T> : IMessage
    {
        List<T> Data { get; }
    }

    public interface ICallbackMessage : IMessage
    {
        event Action OnAccepted;
    }

    public interface ICallbackMessage<out T> : IMessage
    {
        event Action<T> OnAccepted;
    }
}