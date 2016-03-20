using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Persistence
{
  [Serializable]
  public class PersistentDictionary<TKey, TValue> : ISerializable, IDictionary<TKey, TValue>
  {
    private Dictionary<TKey, TValue> data = new Dictionary<TKey, TValue>();

    public PersistentDictionary()
    {
    }

    protected PersistentDictionary(SerializationInfo info, StreamingContext context)
    {
      var enumerator = info.GetEnumerator();

      while (enumerator.MoveNext())
      {
        var stored_data = ((KeyValuePair<TKey, TValue>)enumerator.Value);
        data.Add(stored_data.Key, stored_data.Value);
      }
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      uint i = 0;
      foreach (KeyValuePair<TKey, TValue> pair in data)
        info.AddValue((i++).ToString(), pair);
    }

    public TValue this[TKey key]
    {
      get
      {
        return ((IDictionary<TKey, TValue>)data)[key];
      }

      set
      {
        ((IDictionary<TKey, TValue>)data)[key] = value;
      }
    }

    public int Count
    {
      get
      {
        return ((IDictionary<TKey, TValue>)data).Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return ((IDictionary<TKey, TValue>)data).IsReadOnly;
      }
    }

    public ICollection<TKey> Keys
    {
      get
      {
        return ((IDictionary<TKey, TValue>)data).Keys;
      }
    }

    public ICollection<TValue> Values
    {
      get
      {
        return ((IDictionary<TKey, TValue>)data).Values;
      }
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
      ((IDictionary<TKey, TValue>)data).Add(item);
    }

    public void Add(TKey key, TValue value)
    {
      ((IDictionary<TKey, TValue>)data).Add(key, value);
    }

    public void Clear()
    {
      ((IDictionary<TKey, TValue>)data).Clear();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
      return ((IDictionary<TKey, TValue>)data).Contains(item);
    }

    public bool ContainsKey(TKey key)
    {
      return ((IDictionary<TKey, TValue>)data).ContainsKey(key);
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
      ((IDictionary<TKey, TValue>)data).CopyTo(array, arrayIndex);
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
      return ((IDictionary<TKey, TValue>)data).GetEnumerator();
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
      return ((IDictionary<TKey, TValue>)data).Remove(item);
    }

    public bool Remove(TKey key)
    {
      return ((IDictionary<TKey, TValue>)data).Remove(key);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
      return ((IDictionary<TKey, TValue>)data).TryGetValue(key, out value);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return ((IDictionary<TKey, TValue>)data).GetEnumerator();
    }
  }
}
