using UnityEngine;
using System.Collections;

public class MoneyEarnedGameEvent : GameEvent
{
  public int value = 0;
  public GameEvent source_event;

  public MoneyEarnedGameEvent(int value, GameEvent source_event) : base("money_earned_event")
  {
    this.value = value;
    this.source_event = source_event;
  }

  public MoneyEarnedGameEvent(int value, string name)
    : base(name)
  {
    this.value = value;
    this.source_event = null;
  }
}
