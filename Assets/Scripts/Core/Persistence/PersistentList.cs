using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Persistence
{
  [Serializable]
  public class PersistentList<T> : 
    ISerializable, 
    ILoadFromJson,
    IList<T>
  {
    private List<T> data = new List<T>();

    public PersistentList()
    {

    }

    public void LoadFromJson(string JsonFilePath)
    {
      data = DefaultLoader.DeserializeFromFile<List<T>>(JsonFilePath);
    }

    public PersistentList(SerializationInfo info, StreamingContext context)
    {
      var enumerator = info.GetEnumerator();

      while (enumerator.MoveNext())
      {
        var stored_data = ((T)enumerator.Value);
        data.Add(stored_data);
      }
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      uint i = 0;
      foreach (var element in data)
        info.AddValue((i++).ToString(), element);
    }

    public T this[int index]
    {
      get
      {
        return ((IList<T>)data)[index];
      }

      set
      {
        ((IList<T>)data)[index] = value;
      }
    }

    public int Count
    {
      get
      {
        return ((IList<T>)data).Count;
      }
    }

    public bool IsReadOnly
    {
      get
      {
        return ((IList<T>)data).IsReadOnly;
      }
    }

    public void Add(T item)
    {
      ((IList<T>)data).Add(item);
    }

    public void Clear()
    {
      ((IList<T>)data).Clear();
    }

    public bool Contains(T item)
    {
      return ((IList<T>)data).Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
      ((IList<T>)data).CopyTo(array, arrayIndex);
    }

    public IEnumerator<T> GetEnumerator()
    {
      return ((IList<T>)data).GetEnumerator();
    }

    public int IndexOf(T item)
    {
      return ((IList<T>)data).IndexOf(item);
    }

    public void Insert(int index, T item)
    {
      ((IList<T>)data).Insert(index, item);
    }

    public bool Remove(T item)
    {
      return ((IList<T>)data).Remove(item);
    }

    public void RemoveAt(int index)
    {
      ((IList<T>)data).RemoveAt(index);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return ((IList<T>)data).GetEnumerator();
    }
  }
}
