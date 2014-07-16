using UnityEngine;
using System.Collections;

public class SavedStat : PlinkyObject 
{
  [SerializeField]
  protected string stat_name;
  [SerializeField]
  protected int default_value_on_reset;

  protected int value;
  
  new void Awake()
  {
    LoadEngine();
    Load();
    Debug.Log("SavedStat Awake() called for: " + gameObject.name);
  }

  public virtual void Load()
  {
    if (!IsClone())
    {
      value = PlayerPrefs.GetInt(stat_name, default_value_on_reset);
      Debug.Log(stat_name + " initialized to: " + value);
    }
  }

  public virtual void Save()
  {
    if (!IsClone())
    {
      PlayerPrefs.SetInt(stat_name, value);
      Debug.Log(stat_name + " saved as: " + PlayerPrefs.GetInt(stat_name, 0));
    }
  }

  public virtual void Reset()
  {
    value = default_value_on_reset;
  }

  public int GetValue()
  {
    return value;
  }

  public int GetDefaultValueOnReset()
  {
    return default_value_on_reset;
  }

  private bool IsClone()
  {
    return gameObject.name.Contains("(Clone)");
  }
}
