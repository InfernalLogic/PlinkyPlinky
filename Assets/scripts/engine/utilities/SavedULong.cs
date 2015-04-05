using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SavedULong : MonoBehaviour, ISaveable
{
  protected ulong value;

  [SerializeField]
  protected string key = "";
  [SerializeField]
  protected string default_value_on_reset = "0";
  [SerializeField]
  private bool ignore_soft_reset = false;
  [SerializeField]
  private bool ignore_hard_reset = false;

  private int loaded_value;

  public void ReduceValue(ulong value)
  {
    this.value -= value;
  }

  protected virtual void OnEnable()
  {
    Events.ResetEvents += OnReset;
  }

  protected virtual void OnDisable()
  {
    Events.ResetEvents -= OnReset;
  }

  protected void Awake()
  {
    AssignKey();
    Load();
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
      if (PlayerPrefs.GetInt(key, -1) == -1)
      {
        Debug.Log("No old save found");
        string loaded_value = PlayerPrefs.GetString(key, default_value_on_reset);

        value = Convert.ToUInt64(loaded_value);
      }
      else
      {
        Debug.Log("old save found");
        int loaded_value = PlayerPrefs.GetInt(key, 0);

        value = (ulong)loaded_value;

        PlayerPrefs.DeleteKey(key);
      }
    }
  }

  public virtual void Save()
  {
    if (!IsClone())
    {
      PlayerPrefs.SetString(GetKey(), value.ToString());
    }
  }

  public virtual void OnSerializationEvent()
  {
    AutoSaver.Add(this);
  }

  public void SetName(string name)
  {
    this.name = name;
  }

  protected virtual void OnReset(ResetType type)
  {
    if ((type == ResetType.SOFT && !ignore_soft_reset) ||
        (type == ResetType.HARD && !ignore_hard_reset))
    {
      value = Convert.ToUInt64(loaded_value);
    }
  }

  public ulong GetValue()
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

  public void AddValue(ulong value)
  {
    this.value += value;
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