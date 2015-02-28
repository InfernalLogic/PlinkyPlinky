using UnityEngine;
using System.Collections.Generic;

public class AutoSaver : MonoBehaviour 
{
  private List<SavedStat> stats = new List<SavedStat>();
  private List<SavedStat>.Enumerator enumerator;

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

  public void Add(SavedStat stat)
  {
    stats.Add(stat);
    enumerator = stats.GetEnumerator();
  }

  public void Remove(SavedStat stat)
  {
    stats.Remove(stat);
    enumerator = stats.GetEnumerator();
  }
}
