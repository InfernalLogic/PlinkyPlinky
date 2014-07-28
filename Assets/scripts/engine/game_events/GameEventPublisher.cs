using UnityEngine;
using System.Collections;

public class GameEventPublisher : Singleton<GameEventPublisher>
{
  private Publisher<GameEvent> publisher = new Publisher<GameEvent>();

  public GameEvent bumper_hit_event = new GameEvent("bumper_hit_event");
  public GameEvent peg_hit_event = new GameEvent("peg_hit_event");
  public GameEvent coin_hit_event = new GameEvent("coin_hit_event");
  public GameEvent bomb_dropped_event = new GameEvent("bomb_dropped_event");
  public GameEvent level_loaded_event = new GameEvent("level_loaded_event");
  public GameEvent level_completed_event = new GameEvent("level_completed_event");

  public void AddSubscriber(Subscriber<GameEvent> subscriber)
  {
    publisher.AddSubscriber(subscriber);
  }

  public void PublishMessage(GameEvent message)
  {
    publisher.PublishMessage(message);
  }
}
