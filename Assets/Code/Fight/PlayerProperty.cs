using System;
using System.Collections.Generic;

namespace Car.Fight
{
    public class PlayerProperty : IPlayerProperty
    {
        private string _propertyName;
        private DataType _propertyType;
        private int _value;
        private List<IEnemy> _enemies = new List<IEnemy>();
        private Action<int, DataType> _enemiesHandlers = (int value, DataType type) => { };

        public int Value => _value;

        public PlayerProperty(DataType propertyType, int initialValue = 0)
        {
            _propertyName = Enum.GetName(typeof(DataType), propertyType);
            _propertyType = propertyType;
            _value = initialValue;
        }

        public void Change(bool isAdded)
        {
            if(isAdded)
                _value++;
            else
            {
                _value--;
                if(_value < 0)
                    _value = 0;
            }
            _enemiesHandlers.Invoke(_value, _propertyType);
        }

        public void Add(IEnemy enemy)
        {
            if(!_enemies.Contains(enemy))
            {
                _enemies.Add(enemy);
                _enemiesHandlers += enemy.Update;
            }
        }

        public void Remove(IEnemy enemy)
        {
            if(_enemies.Contains(enemy))
            {
                _enemiesHandlers -= enemy.Update;
                _enemies.Remove(enemy);
            }
        }

        public void RemoveAllEnemies()
        {
            foreach(var enemy in _enemies)
                _enemiesHandlers -= enemy.Update;
            _enemies.Clear();
        }
    }
}
