using UnityEngine;
using System.Collections;

public class BombCooldownTimer : Timer 
{
  [SerializeField]
  private float base_duration = 1.0f;

  private Subscriber<GameEvent> subscriber = new Subscriber<GameEvent>();

  void Awake()
  {
    UpgradeEvents.AddSubscriber(subscriber);
    GameEvents.AddSubscriber(subscriber);
  }

  void Start()
  {
    RecalculateDuration();
  }

  void Update()
  {
    while (!subscriber.IsEmpty())
    {
      if (subscriber.ReadNewestMessage() == UpgradeEvents.bomb_cooldown_upgraded || subscriber.ReadNewestMessage() == GameEvents.game_reset_event)
      {
        RecalculateDuration();
      }
        
      subscriber.DeleteNewestMessage();
    }
  }

  private void RecalculateDuration()
  {
    SetDuration(base_duration - FindObjectOfType<BombCooldownUpgrader>().GetCooldownReduction());
  }

  public float GetDuration()
  {
    return duration;
  }
}
