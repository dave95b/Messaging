using UnityEngine;
using UnityEditor;

namespace Messaging
{
    public static class MessageBoxFactory
    {
        public static IReadableMessageBox Create() => new MessageBox();
    }

    public static class PostOfficeFactory
    {
        public static IPostOffice Create() => new PostOffice();
    }
}