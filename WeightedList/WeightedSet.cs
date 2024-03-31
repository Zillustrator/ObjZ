using System;
using System.Collections;
using System.Collections.Generic;

namespace ObjZ.Collections.Generic
{
    public class WeightedSet<T> : IEnumerable
    {
        private Random _random;
        private Dictionary<T, uint> _values = new Dictionary<T, uint>();
        private uint _weight = 0;

        public WeightedSet()
        {
            _random = new Random();
        }

        public WeightedSet(int seed)
        {
            _random = new Random(seed);
        }

        public uint this[T item]
        {
            get => _values[item];
            set
            {
                if (_values.TryGetValue(item, out var weight))
                {
                    _weight -= weight;
                }

                _values[item] = value;

                _weight += value;
            }
        }

        public int Seed { set { _random = new Random(value); } }

        public int Weight => (int)_weight;

        public int Count => _values.Count;

        public void Add(T item, uint weight)
        {
            _values.Add(item, weight);

            _weight += weight;
        }

        public bool Remove(T item)
        {
            if (_values.Remove(item, out var weight))
            {
                _weight -= weight;

                return true;
            }

            return false;
        }

        public void Clear()
        {
            _values.Clear();

            _weight = 0;
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)_values).GetEnumerator();
        }

        public T Random
        {
            get
            {
                uint temp = 0;
                var random = _random.Next(0, Weight);

                foreach (var item in _values)
                {
                    temp += item.Value;

                    if (random < temp) return item.Key;
                }

                throw new WeightedSetException();
            }
        }
    }
}

