using System;
using System.Collections.Generic;

namespace Car.Inventory
{
    public interface IInventoryController : IControllerBase
    {
        void Show(Action exitHandler);
        void Hide();
    }
}
