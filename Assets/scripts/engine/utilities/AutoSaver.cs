using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoSaver : Singleton<AutoSaver>
{
  private static List<ISaveable> stats = new List<ISaveable>();
  private static List<ISaveable>.Enumerator enumerator;

  private void Start()
  {
    enumerator = stats.GetEnumerator();
    StartCoroutine(SerializeTimer());
  }

  private static void SerializeAll()
  {
    foreach (ISaveable stat in stats)
      stat.Save();

    Debug.Log("saved " + stats.Count);
    AutoSaver.Instance.StartCoroutine(SerializeTimer());
  }

  private static IEnumerator SerializeTimer()
  {
    yield return new WaitForSeconds(3.0f);
    AutoSaver.SerializeAll();
  }

  public static void Add(ISaveable stat)
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
