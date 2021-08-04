using System;
using System.Collections.Generic;

namespace Car.Inventory
{
    public interface IInventoryController : IControllerBase
    {
        event Action Exit;
        void Show();
    }
}
