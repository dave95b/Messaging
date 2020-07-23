using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Messaging
{
    public interface IMessage
    {
        int Count { get; }
    }

    public interface IMessage<out T> : IMessage
    {
        IReadOnlyList<T> Data { get; }
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
        public int Count { get; private set; }
    }
}