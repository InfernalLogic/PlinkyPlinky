using UnityEngine;
using System.Collections;

public class MoneyTracker : Singleton<MoneyTracker> 
{
  [SerializeField]
  private SavedStat current_money;

  private Subscriber<GameEvent> game_event_listener = new Subscriber<GameEvent>();

  new void Awake()
  {
    base.Awake();
    GameEventPublisher.AddSubscriber(game_event_listener);
  }

  public int GetCurrentMoney()
  {
    if (current_money != null)
      return current_money.GetValue();
    else
      return -1;
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
  }

  private string CurrentEventKey()
  {
    return game_event_listener.ReadNewestMessage().name;
  }

  private void PublishMoneyEarnedEvent()
  {
    GameEventPublisher.PublishMessage(CurrentMoneyEarnedEvent());
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
}
