using System;
using System.Collections.Generic;
using TopDownHordes.Interfaces;

namespace Utilities
{
    public class RandomSelector<T> : ISectionSelector<T> 
    {
        private readonly Queue<int> _recentIndices;
        private readonly Random _random;
        private readonly T[] _items;
        
        private readonly int _memory;
        
        public RandomSelector(T[] items, int memory = 2)
        {
            if (memory >= items.Length)
            {
                throw new ArgumentException("The memory should be less than the number of items");
            }
            
            _recentIndices = new Queue<int>(memory);
            _random = new Random();
            _memory = memory;
            _items = items;
        }

        public T GetRandomItem() 
        {
            int index;
            do 
            {
                index = _random.Next(_items.Length);
            } 
            while (_recentIndices.Contains(index));
            
            // Remove the oldest index if the queue is full
            if (_recentIndices.Count == _memory)
                _recentIndices.Dequeue();

            _recentIndices.Enqueue(index);
        
            return _items[index];
        }
    }
}