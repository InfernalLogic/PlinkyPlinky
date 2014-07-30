using UnityEngine;
using System.Collections;

public class SavedBool : SavedStat 
{
  public void Set(bool value)
  {
    if (value)
      this.value = 1;
    else
      this.value = 0;
  }

  public bool IsTrue()
  {
    return value == 1;
  }

  public bool IsFalse()
  {
    return value == 0;
  }
}
