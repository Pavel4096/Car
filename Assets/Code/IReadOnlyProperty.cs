﻿using System;

namespace Car
{
    public interface IReadOnlyProperty<T>
    {
        T Value { get; }
        void Subscribe(Action<T> method);
        void Unsubscribe(Action<T> method);
    }
}
