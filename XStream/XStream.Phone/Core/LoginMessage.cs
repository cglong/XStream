using GalaSoft.MvvmLight.Messaging;
using System;
using Telerik.Windows.Controls;

namespace XStream.Phone.Core
{
    public class LoginMessage : MessageBase
    {
        public LoginMessage(Action<InputPromptClosedEventArgs> callback)
        {
            this.Callback = callback;
        }

        public Action<InputPromptClosedEventArgs> Callback { get; private set; }
    }
}
