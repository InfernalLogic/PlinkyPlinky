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
      if (UpgradeHandler.Instance().ContainsKey(CurrentEventKey()))
      {
        AddMoney(UpgradeHandler.Instance().FindScoringObjectByKey(CurrentEventKey()).GetPointValue());
        PublishMoneyEarnedEvent();
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

  private void PublishMoneyEarnedEvent()
  {
    GameEvents.Publish(CurrentMoneyEarnedEvent());

  }

  private void PublishPlinkagonPointEarnedEvent()
  {
    GameEvents.Publish(CurrentPlinkagonPointEvent());
  }

  private MoneyEarnedGameEvent CurrentPlinkagonPointEvent()
  {
    return new MoneyEarnedGameEvent(achievement_listener.ReadNewestMessage().popup_info.plinkagon_point_value, "plinkagon_point_earned_event");
  }

  private MoneyEarnedGameEvent CurrentMoneyEarnedEvent()
  {
    return new MoneyEarnedGameEvent(UpgradeHandler.Instance().FindScoringObjectByKey(CurrentEventKey()).GetPointValue(), game_event_listener.ReadNewestMessage());
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
}
