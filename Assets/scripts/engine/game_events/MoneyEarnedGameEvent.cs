using UnityEngine;
using System.Collections;

public class MoneyEarnedGameEvent : GameEvent
{
  public int value = 0;

  public MoneyEarnedGameEvent(int value, GameEvent source_event) : base("money_earned_event")
  {
    this.value = value;
  }
}
