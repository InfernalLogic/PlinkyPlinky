using UnityEngine;
using System.Collections;

public class SavedStat : MonoBehaviour
{
  protected int value;

  [SerializeField]
  protected string stat_name = "";
  [SerializeField]
  protected int default_value_on_reset;
  [SerializeField]
  private bool ignore_reset = false;

  private int loaded_value;

  new void Awake()
  {
    if (stat_name == "")
      stat_name = gameObject.name;

    Load();
  }

  public virtual void Load()
  {
    if (!IsClone())
    {
      loaded_value = PlayerPrefs.GetInt(stat_name, default_value_on_reset);
      value = loaded_value;
      Debug.Log(GetKey() + " loaded: " + value);
    }
    else
    {
      Debug.LogError("Ignored clone for: " + GetKey());
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

  public int GetValue()
  {
    return value;
  }

  public string GetKey()
  {
    if (stat_name != "")
      return stat_name;
    else
      return gameObject.name;
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