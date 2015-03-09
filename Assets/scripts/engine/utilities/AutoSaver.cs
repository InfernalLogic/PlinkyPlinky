using UnityEngine;
using System.Collections.Generic;

public class AutoSaver : MonoBehaviour 
{
  private List<ISaveable> stats = new List<ISaveable>();
  private List<ISaveable>.Enumerator enumerator;

  private void Start()
  {
    enumerator = stats.GetEnumerator();
  }

  private void Update()
  {
    if (!enumerator.MoveNext())
    {
      enumerator = stats.GetEnumerator();
      enumerator.MoveNext();
    }
    
    enumerator.Current.Save();
  }

  public void Add(ISaveable stat)
  {
    stats.Add(stat);
    enumerator = stats.GetEnumerator();
  }

  public void Remove(ISaveable stat)
  {
    stats.Remove(stat);
    enumerator = stats.GetEnumerator();
  }
}
