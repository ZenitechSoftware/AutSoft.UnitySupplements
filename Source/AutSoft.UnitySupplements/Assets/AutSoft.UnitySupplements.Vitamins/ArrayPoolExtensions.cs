using System;
using System.Buffers;

namespace AutSoft.UnitySupplements.Vitamins
{
    public static class ArrayPoolExtensions
    {
        public static PooledArray<T> RentDisposable<T>(this ArrayPool<T> pool, int minSize = 200) => new(minSize, pool.Rent(minSize), pool);
    }

    /// <summary>
    /// A wrapper around <see cref="System.Buffers.ArrayPool{T}"/> to automatically return to the pool upon disposal.
    /// </summary>
    public sealed class PooledArray<T> : IDisposable
    {
        private readonly ArrayPool<T> _pool;
        private int _size;

        public PooledArray(int minSize, T[] values, ArrayPool<T> pool)
        {
            _size = minSize;
            Values = values;
            _pool = pool;
        }

        public int Size
        {
            get => _size;
            set
            {
                if (_size < value) throw new ArgumentOutOfRangeException(nameof(value), $"When setting a new value for {nameof(Size)} it can only be smaller or equal to the current value. Current: {Size} set value: {value}");
                _size = value;
            }
        }

        public T[] Values { get; }

        /// <summary>
        /// Returns the array to the pool.
        /// </summary>
        public void Dispose() => _pool.Return(Values);

        public T this[int index]
        {
            get => Values[index];
            set => Values[index] = value;
        }
    }
}
