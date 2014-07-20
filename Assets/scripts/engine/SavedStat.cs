using UnityEngine;
using System.Collections;

public class SavedStat : MonoBehaviour 
{
  protected int value;

  [SerializeField]
  protected string stat_name;
  [SerializeField]
  protected int default_value_on_reset;

  private int loaded_value;
  
  new void Awake()
  {
    if (stat_name == null)
      stat_name = gameObject.name;

    Load();
  }

  public virtual void Load()
  {
    if (!IsClone())
    {
      loaded_value = PlayerPrefs.GetInt(stat_name, default_value_on_reset);
      value = loaded_value;
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

  public void SetName(string name)
  {
    this.name = name;
  }

  public virtual void Reset()
  {
    value = default_value_on_reset;
    Debug.Log(name + " reset");
  }

  public int GetValue()
  {
    return value;
  }

  public void AddValue(int value)
  {
    this.value += value;
  }

  public int GetDefaultValueOnReset()
  {
    return default_value_on_reset;
  }

  private bool IsClone()
  {
    return gameObject.name.Contains("(Clone)");
  }

  void OnDestroy()
  {
    Save();
  }
}
