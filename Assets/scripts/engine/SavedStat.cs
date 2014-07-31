using UnityEngine;
using System.Collections;

public class SavedStat : MonoBehaviour
{
  protected int value;

  [SerializeField]
  protected string stat_key = "";
  [SerializeField]
  protected int default_value_on_reset;
  [SerializeField]
  private bool ignore_reset = false;
  [SerializeField]
  private bool ignore_hard_reset = false;

  private int loaded_value;

  protected void Awake()
  {
    AssignKey();
    Load();
  }

  private void AssignKey()
  {
    if (stat_key.Length < 1)
      stat_key = gameObject.name;
  }

  public virtual void Load()
  {
    if (!IsClone())
    {
      loaded_value = PlayerPrefs.GetInt(stat_key, default_value_on_reset);
      value = loaded_value;
    }
  }

  public virtual void Save()
  {
    if (!IsClone())
    {
      PlayerPrefs.SetInt(GetKey(), value);
    }
  }

  public void SetName(string name)
  {
    this.name = name;
  }

  public virtual void Reset()
  {
    if (!ignore_reset)
    {
      value = default_value_on_reset;
      Debug.Log(GetKey() + " reset");
    }
  }

  public virtual void HardReset()
  {
    if (!ignore_hard_reset)
    {
      value = default_value_on_reset;
      Debug.Log(GetKey() + " hard reset");
    }
  }

  public int GetValue()
  {
    return value;
  }

  public string GetKey()
  {
    if (stat_key.Length > 0)
      return stat_key;
    else
    {
      Debug.LogError("Stat not properly renamed: " + gameObject.name);
      return gameObject.name;
    }
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