using UnityEngine;
using System.Collections;

public class MoneyTracker : Singleton<MoneyTracker> 
{
  [SerializeField]
  private SavedStat current_money;
  [SerializeField]
  private SavedStat current_plinkagon_points;

  private Subscriber<GameEvent> game_event_listener = new Subscriber<GameEvent>();
  private Subscriber<AchievementUnlocked> achievement_listener = new Subscriber<AchievementUnlocked>();

  new void Awake()
  {
    base.Awake();
    GameEvents.AddSubscriber(game_event_listener);
    AchievementUnlockedEvents.AddSubscriber(achievement_listener);
  }

  void Update()
  {
    while (!game_event_listener.IsEmpty())
    {
      if (CurrentEventKey() == "coin_hit_event")
      {
        CoinHitEvent coin_event = (CoinHitEvent)game_event_listener.ReadNewestMessage();

        int money_to_add = coin_event.GetMultiplier() * UpgradeHandler.Instance.FindScoringObjectByKey(CurrentEventKey()).GetPointValue();

        AddMoney(money_to_add);
        PublishMoneyEarnedEvent(money_to_add);
      }
      else if (UpgradeHandler.Instance.ContainsKey(CurrentEventKey()))
      {
        int money_to_add = UpgradeHandler.Instance.FindScoringObjectByKey(CurrentEventKey()).GetPointValue();
        AddMoney(money_to_add);
        PublishMoneyEarnedEvent(money_to_add);
      }
      else if (CurrentEventKey() == "plinkagon_point_earned_event")
      {
        MoneyEarnedGameEvent money_earned_event = (MoneyEarnedGameEvent)game_event_listener.ReadNewestMessage();
        AddPlinkagonPoints(money_earned_event.value);
      }
      game_event_listener.DeleteNewestMessage();
    }
  }

  private string CurrentEventKey()
  {
    return game_event_listener.ReadNewestMessage().name;
  }

  private void PublishMoneyEarnedEvent(int money_to_add)
  {
    GameEvents.Publish(CurrentMoneyEarnedEvent(money_to_add));
  }

  private void PublishPlinkagonPointEarnedEvent()
  {
    GameEvents.Publish(CurrentPlinkagonPointEvent());
  }

  private MoneyEarnedGameEvent CurrentPlinkagonPointEvent()
  {
    return new MoneyEarnedGameEvent(achievement_listener.ReadNewestMessage().popup_info.plinkagon_point_value, "plinkagon_point_earned_event");
  }

  private MoneyEarnedGameEvent CurrentMoneyEarnedEvent(int money_to_add)
  {
    return new MoneyEarnedGameEvent(money_to_add, game_event_listener.ReadNewestMessage());
  }

  public void AddMoney(int income)
  {
    current_money.AddValue(income);
  }

  public void AddPlinkagonPoints(int income)
  {
    current_plinkagon_points.AddValue(income);
  }

  public void SpendPlinkagonPoints(int price)
  {
    current_plinkagon_points.AddValue(-price);
  }

  public void SpendMoney(int price)
  {
    current_money.AddValue(-price);
  }

  public int GetCurrentMoney()
  {
    if (current_money != null)
      return current_money.GetValue();
    else
      return -1;
  }

  public int GetCurrentPlinkagonPoints()
  {
    if (current_plinkagon_points != null)
      return current_plinkagon_points.GetValue();
    else
      return -1;
  }

  public void ResetPlinkagonPoints()
  {
    int points_spent = 0;
    PlinkagonUpgrade[] upgrades = FindObjectsOfType<PlinkagonUpgrade>();

    foreach (PlinkagonUpgrade upgrade in upgrades)
    {
      points_spent += upgrade.GetValue();
      upgrade.HardReset();
    }

    AddPlinkagonPoints(points_spent);
  }
}
