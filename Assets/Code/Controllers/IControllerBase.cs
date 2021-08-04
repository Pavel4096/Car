using System;

namespace Car
{
    public interface IControllerBase : IDisposable
    {
        void AddController(IControllerBase controller);
    }
}
