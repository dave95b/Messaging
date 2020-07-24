using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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

    internal class Message : IMessage
    {
        public int Count { get; set; }

        public bool Equals(IMessage other) => this == other;
    }
}