using System;
using System.Collections.Generic;

namespace Messaging
{
    public interface IPostOffice : IMessageBox
    {
        void Register<TMessage>(IMessageBox box)
            where TMessage : IMessage;

        void Unregister<TMessage>(IMessageBox box)
            where TMessage : IMessage;
    }

    internal class PostOffice : IPostOffice
    {
        private readonly Dictionary<Type, List<IMessageBox>> boxes;

        internal PostOffice()
        {
            boxes = new Dictionary<Type, List<IMessageBox>>();
        }


        public void Register<TMessage>(IMessageBox box)
            where TMessage : IMessage
        {
            var boxList = GetBoxes<TMessage>();
            boxList.Add(box);
        }

        public void Unregister<TMessage>(IMessageBox box) 
            where TMessage : IMessage
        {
            var boxList = GetBoxes<TMessage>();
            boxList.Remove(box);
        }

        public void SendSimple<TMessage>()
            where TMessage : IMessage, new()
        {
            var boxList = GetBoxes<TMessage>();
            foreach (var box in boxList)
                box.SendSimple<TMessage>();
        }

        public void SendWithData<TMessage, TData>(TData data) 
            where TMessage : IDataMessage<TData>, new()
        {
            var boxList = GetBoxes<TMessage>();
            foreach (var box in boxList)
                box.SendWithData<TMessage, TData>(data);
        }

        public void SendWithCallback<TMessage>(Action callback) 
            where TMessage : ICallbackMessage, new()
        {
            var boxList = GetBoxes<TMessage>();
            foreach (var box in boxList)
                box.SendWithCallback<TMessage>(callback);
        }

        public void SendWithCallback<TMessage, TCallback>(Action<TCallback> callback)
            where TMessage : ICallbackMessage<TCallback>, new()
        {
            var boxList = GetBoxes<TMessage>();
            foreach (var box in boxList)
                box.SendWithCallback<TMessage, TCallback>(callback);
        }

        public void SendRich<TMessage, TData>(TData data, Action callback)
            where TMessage : IDataMessage<TData>, ICallbackMessage, new()
        {
            var boxList = GetBoxes<TMessage>();
            foreach (var box in boxList)
                box.SendRich<TMessage, TData>(data, callback);
        }

        public void SendRich<TMessage, TData, TCallback>(TData data, Action<TCallback> callback) 
            where TMessage : IDataMessage<TData>, ICallbackMessage<TCallback>, new()
        {
            var boxList = GetBoxes<TMessage>();
            foreach (var box in boxList)
                box.SendRich<TMessage, TData, TCallback>(data, callback);
        }

        private List<IMessageBox> GetBoxes<T>()
        {
            var type = typeof(T);
            if (boxes.TryGetValue(type, out var boxList))
                return boxList;

            boxList = new List<IMessageBox>();
            boxes[type] = boxList;

            return boxList;
        }
    }
}