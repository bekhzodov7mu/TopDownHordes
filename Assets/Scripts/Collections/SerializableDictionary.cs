using System;
using System.Collections.Generic;
using Collections.Scopes;
using UnityEngine;

namespace Collections
{
    [Serializable]
    public sealed partial class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [HideInInspector]
        [SerializeField]
        private Entry[] _entries = new Entry[0];

        [HideInInspector]
        [SerializeField]
        private TKey[] _keys = new TKey[0];

        [HideInInspector]
        [SerializeField]
        private TValue[] _values = new TValue[0];

        public SerializableDictionary()
        {
        }

        public SerializableDictionary(IEnumerable<KeyValuePair<TKey, TValue>> values)
        {
            foreach (var (key, value) in values)
            {
                Add(key, value);
            }
        }
        public SerializableDictionary(int capacity)
        {
            _entries = new Entry[capacity];
            _keys = new TKey[capacity];
            _values = new TValue[capacity];
        }

        private void Convert()
        {
            if (_keys.Length == 0 && _values.Length == 0)
            {
                return;
            }

            using (ListScope<Entry>.Create(out var entries))
            {
                var count = Mathf.Min(_keys.Length, _values.Length);

                for (var i = 0; i < count; i++)
                {
                    entries.Add(new Entry
                    {
                        Key = _keys[i],
                        Value = _values[i]
                    });
                }

                _entries = entries.ToArray();
                _keys = new TKey[0];
                _values = new TValue[0];
            }
        }

        #region ISerializationCallbackReceiver

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            Clear();
            Convert();

            foreach (var entry in _entries)
            {
                this[entry.Key] = entry.Value;
            }
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            using (ListScope<Entry>.Create(out var entries))
            {
                foreach (var (key, value) in this)
                {
                    entries.Add(new Entry
                    {
                        Key = key,
                        Value = value
                    });
                }

#if UNITY_EDITOR
                if (typeof(TKey).IsEnum)
                {
                    entries.Sort((x, y) => Comparer<int>.Default.Compare((int)(object)x.Key, (int)(object)y.Key));
                }
                else if (typeof(IComparable<TKey>).IsAssignableFrom(typeof(TKey)))
                {
                    entries.Sort((x, y) => Comparer<TKey>.Default.Compare(x.Key, y.Key));
                }
#endif

                _entries = entries.ToArray();
            }
        }

        #endregion
    }
}