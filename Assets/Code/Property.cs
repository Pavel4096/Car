using System;

namespace Car
{
    public class Property<T> : IReadOnlyProperty<T>
    {
        private T propertyValue;
        private Action<T> handlers;

        public T Value
        {
            get => propertyValue;
            set
            {
                propertyValue = value;
                handlers?.Invoke(propertyValue);
            }
        }

        public void Subscribe(Action<T> handler)
        {
            handlers += handler;
        }

        public void Unsubscribe(Action<T> handler)
        {
            handlers -= handler;
        }
    }

    public class Property : IReadOnlyProperty
    {
        private Action handlers;

        public void Subscribe(Action handler)
        {
            handlers += handler;
        }

        public void Unsubscribe(Action handler)
        {
            handlers += handler;
        }

        public void Inform()
        {
            handlers?.Invoke();
        }
    }
}
