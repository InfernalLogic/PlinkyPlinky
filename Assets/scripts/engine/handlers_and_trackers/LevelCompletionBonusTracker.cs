using UnityEngine;
using System.Collections;

public class LevelCompletionBonusTracker : Singleton<LevelCompletionBonusTracker> 
{
  [SerializeField]
  private SavedStat total_levels_unlocked;

  private int stored_cash = 0;
  private int temp_storage = 0;
  private Subscriber<GameEvent> subscriber = new Subscriber<GameEvent>();

  private void Start()
  {
    GameEvents.AddSubscriber(subscriber);
    StartCoroutine(DumpTempStorage());

    int current_levels_unlocked = FindObjectOfType<LevelUnlocker>().GetTotalUnlockedLevels();

    if (total_levels_unlocked.GetValue() < current_levels_unlocked - 3)
      total_levels_unlocked.AddValue(current_levels_unlocked);
  }

  private void OnEnable()
  {
    Events.ResetEvents += OnReset;
  }

  private void OnDisable()
  {
    Events.ResetEvents += OnReset;
  }

  private void Update()
  {
    while (!subscriber.IsEmpty())
    {
      if (CurrentEventKey() == "money_earned_event")
        temp_storage += ((MoneyEarnedGameEvent)subscriber.ReadNewestMessage()).value;
      
      if (CurrentEventKey() == GameEvents.level_completed_event.name)
      {
        int bonus = CalculateBonus();

        if (bonus > 0)
          AnnounceBonus(bonus);

        MoneyTracker.Instance.AddMoney(bonus);
      }

      subscriber.DeleteNewestMessage();
    }
  }

  private void AnnounceBonus(int bonus)
  {
    AchievementFloatingTextFactory.Instance.Announce("Level Complete Bonus\n(" +
      total_levels_unlocked.GetValue() + "%): " + bonus);
  }

  private IEnumerator DumpTempStorage()
  {
    yield return new WaitForSeconds(5.0f);
    stored_cash += temp_storage;
    StartCoroutine(CashTimeOut(temp_storage));
    temp_storage = 0;
    StartCoroutine(DumpTempStorage());
  }

  private IEnumerator CashTimeOut(int value)
  {
    yield return new WaitForSeconds(30.0f);
    stored_cash -= value;
    if (stored_cash < 0)
      stored_cash = 0;
  }

  private string CurrentEventKey()
  {
    return subscriber.ReadNewestMessage().name;
  }

  private int CalculateBonus()
  {
    return (int)(stored_cash * ((float)total_levels_unlocked.GetValue() / 100.0f));
  }

  public void AddUnlockedLevel()
  {
    total_levels_unlocked.AddValue(1);
  }

  private void OnReset(ResetType type)
  {
    stored_cash = 0;
    temp_storage = 0;
  }
}
