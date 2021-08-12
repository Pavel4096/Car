using System;
using System.Collections.Generic;

namespace Car
{
    public class ItemFactory
    {}
    
    public class ItemConfigsFactory
    {
        private Dictionary<Type, ItemFactory> factories = new Dictionary<Type, ItemFactory>();
    }
}
