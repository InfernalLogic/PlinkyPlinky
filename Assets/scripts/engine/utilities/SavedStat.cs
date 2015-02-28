using UnityEngine;
using System.Collections.Generic;

public class SavedStat : MonoBehaviour
{
  protected int value;

  [SerializeField]
  protected string key = "";
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

    FindObjectOfType<AutoSaver>().Add(this);
  }

  private void AssignKey()
  {
    if (key.Length < 1)
      key = gameObject.name;
  }

  public virtual void Load()
  {
    if (!IsClone())
    {
      loaded_value = PlayerPrefs.GetInt(key, default_value_on_reset);
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
    if (key.Length > 0)
      return key;
    else
    {
      key = gameObject.name;
      return key;
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
    AutoSaver auto_saver = FindObjectOfType<AutoSaver>();

    if (auto_saver)
      auto_saver.Remove(this);

    Save();
  }
}