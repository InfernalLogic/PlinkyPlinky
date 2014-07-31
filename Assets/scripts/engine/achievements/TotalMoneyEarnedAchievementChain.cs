using UnityEngine;
using System.Collections;

public class TotalMoneyEarnedAchievementChain : AchievementChain 
{
  public override void HandleGameEvent(GameEvent game_event)
  {
    MoneyEarnedGameEvent money_earned_event = (MoneyEarnedGameEvent)game_event;
    AddValue(money_earned_event.value);
  }
}
