using UnityEngine;
using System.Collections;

public class CoinHitEvent : GameEvent 
{
  private int multiplier;

  public CoinHitEvent(int multiplier)
    : base("coin_hit_event")
  {
    this.multiplier = multiplier;
  }

  public int GetMultiplier()
  {
    return multiplier;
  }
}
