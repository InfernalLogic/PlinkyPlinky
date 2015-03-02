using UnityEngine;
using System.Collections;

public class BombCooldownTimer : Timer 
{
  [SerializeField]
  private float base_duration = 1.0f;

  private Subscriber<GameEvent> subscriber = new Subscriber<GameEvent>();

  private void Awake()
  {
    UpgradeEvents.AddSubscriber(subscriber);
    GameEvents.AddSubscriber(subscriber);
  }

  private void OnEnable()
  {
    Events.ResetEvents += OnReset;
  }

  private void OnDisable()
  {
    Events.ResetEvents -= OnReset;
  }

  private void Start()
  {
    RecalculateDuration();
  }

  private void Update()
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

  private void OnReset(ResetType type)
  {
    StartCoroutine(RecalculateDurationNextFrame());
  }

  private IEnumerator RecalculateDurationNextFrame()
  {
    yield return new WaitForFixedUpdate();
    RecalculateDuration();
  }
}
