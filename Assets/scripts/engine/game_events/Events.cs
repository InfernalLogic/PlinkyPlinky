using UnityEngine;
using System.Collections;

public enum ResetType { SOFT, HARD };

public class Events : Singleton<Events>
{
  #region Reset
  public delegate void Reset_(ResetType type);
  public static event Reset_ ResetEvents;
  public static void PublishReset(ResetType type)
  {
    if (ResetEvents != null)
      ResetEvents(type);
  }
  #endregion
}
