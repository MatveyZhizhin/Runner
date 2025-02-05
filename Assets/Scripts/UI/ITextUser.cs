using System;

namespace UI
{
    public interface ITextUser
    {
        public event Action<string> Changed;
    }
}

