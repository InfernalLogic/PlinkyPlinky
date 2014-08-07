using UnityEngine;
using System.Collections;

public class MoneyTracker : Singleton<MoneyTracker> 
{
  [SerializeField]
  private SavedStat current_money;
  [SerializeField]
  private SavedStat current_plinkagon_points;
  [SerializeField]
  private FloatingText plinkagon_floating_text;

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
        game_event_listener.DeleteNewestMessage();
      }
      else
      {
        game_event_listener.DeleteNewestMessage();
      }
    }

    while (!achievement_listener.IsEmpty())
    {
      AchievementPopupInfo popup_info = achievement_listener.ReadNewestMessage().popup_info;
      current_plinkagon_points.AddValue(popup_info.plinkagon_point_value);
      FloatingText new_text = (FloatingText)Instantiate(plinkagon_floating_text, Vector3.zero, transform.rotation);
      new_text.SetText("  " + popup_info.achievement_text_number + " " + popup_info.achievement_text + "\n+" + popup_info.plinkagon_point_value + " plinkagon points!");
      PublishPlinkagonPointEarnedEvent();
      achievement_listener.DeleteNewestMessage();
      Debug.Log("current_plinkagon_points: " + current_plinkagon_points.GetValue());
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
    Debug.Log("plinkagon_point_earned published: +" + achievement_listener.ReadNewestMessage().popup_info.plinkagon_point_value);
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
